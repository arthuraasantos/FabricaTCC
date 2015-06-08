using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEnd.Controllers;
using Seedwork.Const;

namespace FrontEnd.Models
{
    public class HorarioDeExpedienteController : BaseController<HorarioDeExpediente,HorarioDeExpedienteNovo,HorarioDeExpedienteEditar>
    {
        // GET: HorarioDeExpediente
        public IHorarioDeExpedienteRepository HorarioDeExpedienteRepository;
        public IEmpresaRepository EmpresaRepository;

        private IEnumerable<SelectListItem> ListaEmpresas;


        public HorarioDeExpedienteController(MyContext context, IHorarioDeExpedienteRepository horarioDeExpedienteRepository, IEmpresaRepository empresaRepository)
            : base(context, horarioDeExpedienteRepository, new HorarioDeExpedienteToHorarioDeExpedienteNovo(empresaRepository), new HorarioDeExpedienteToHorarioDeExpedienteEditar(empresaRepository))
        {
            HorarioDeExpedienteRepository = horarioDeExpedienteRepository;
            EmpresaRepository = empresaRepository;

            switch (Sessao.PerfilFuncionarioLogado)
            {

                case PerfilAcesso.Administrador: // Traz todos as Empresas
                    ListaEmpresas = EmpresaRepository
                                    .Listar()
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.NomeFantasia, Value = p.Id.ToString() });
                    break;
                default:
                    ListaEmpresas = EmpresaRepository
                                    .Listar()
                                    .Where(e => e.Id == Sessao.EmpresaLogada.Id)
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.NomeFantasia, Value = p.Id.ToString() });
                    break;
            }
        }

        public override ActionResult Index()
        {
            List<HorarioDeExpediente> lista = new List<HorarioDeExpediente>();

            if (Sessao.PerfilFuncionarioLogado == PerfilAcesso.Administrador)// Se for administrador do sistema, mostrar todas as Empresas
            {
                lista = HorarioDeExpedienteRepository.Listar().ToList();
            }
            else
            {
                lista = HorarioDeExpedienteRepository.Listar().Where(f => f.Empresa.Id == Sessao.EmpresaLogada.Id).ToList();
            }

            return View("Index", lista);
        }

        public override ActionResult Visualizar(Guid Id)
        {
            try
            {
                ViewBag.ListagemdeEmpresas = ListaEmpresas;
                Empresa e = new Empresa();
                HorarioDeExpediente h = new HorarioDeExpediente();
                h = HorarioDeExpedienteRepository.PesquisarPeloId(Id);
                e = EmpresaRepository.PesquisarPeloId(h.Empresa.Id);
                ViewBag.Empresa = e.NomeFantasia;
                return base.Visualizar(Id);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro ao editar funcionário. " + e.Message;
                return RedirectToAction("Index");
            }
        }

        public override ActionResult Novo()
        {

            Empresa emp = (Empresa)TempData["Empresa"];

            var novo = new HorarioDeExpedienteNovo()
            {
                Empresas = ListaEmpresas,
            };

            if (emp != null)
            {
                novo.IdEmpresa = emp.Id;
            }

            return View("Novo", novo);
        }

        public override ActionResult Incluir(HorarioDeExpedienteNovo novo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (novo.IdEmpresa == null)
                    {
                        novo.Id = Sessao.EmpresaLogada.Id;
                    }
                    var entity = ConversorInsert.Converter(novo);
                    entity.Id = Guid.NewGuid();

                    Repository.Salvar(entity);
                    Context.SaveChanges();

                    TempData["Mensagem"] = "Horário de Expediente cadastrado com sucesso!";
                }
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao cadastrar Horário de Expediente!";
                return RedirectToAction("Index");
            }
        }

        public override ActionResult Editar(HorarioDeExpedienteEditar editar)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var entity = Repository.PesquisarPeloId(editar.Id);
                    ConversorEdit.AplicarValores(editar, entity);
                    Repository.Salvar(entity);
                    Context.SaveChanges();
                    TempData["Mensagem"] = "Horário de Expediente alterado com sucesso!";
                }
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro ao alterar o Horário de Expediente! " + e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}