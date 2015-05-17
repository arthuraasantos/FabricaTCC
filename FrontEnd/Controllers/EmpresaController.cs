using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using FrontEnd.Models.Conversores;
using Infraestrutura;
using Infraestrutura.Repositorios;
using Seedwork.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;

namespace FrontEnd.Controllers
{
    public class EmpresaController : BaseController<Empresa, EmpresaNovo, EmpresaEditar>
    {
        public IEmpresaRepository EmpresaRepository { get; set; }
        public EmpresaController(MyContext context, IEmpresaRepository empresaRepository, IFuncionarioRepository funcionarioRepository)
            : base(context, empresaRepository, new EmpresaToEmpresaNovo(), new EmpresaToEmpresaEditar())
        {
            EmpresaRepository = empresaRepository;
        }

        public override ActionResult Index()
        {
            List<Empresa> lista = new List<Empresa>();

            if (Sessao.PerfilFuncionarioLogado == PerfilAcesso.Administrador)// Se for administrador do sistema, mostrar todas as Empresas
            {
                lista = EmpresaRepository.Listar().ToList();
            }
            else
            {
                lista = EmpresaRepository.Listar().Where(f => f.Id == Sessao.EmpresaLogada.Id).ToList();
            }

            return View("Index", lista);
        }

        public override ActionResult Incluir(EmpresaNovo novo)
        {
            try
            {
                var entity = ConversorInsert.Converter(novo);
                entity.Id = Guid.NewGuid();
                entity.Bloqueado = "N";
                Repository.Salvar(entity);
                Context.SaveChanges();
                TempData["Mensagem"] = "Empresa cadastrada com sucesso!";
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao cadastrar empresa!";
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public JsonResult BloquearEmpresa(string Id)
        {
            try
            {
                var idempresa = Id.Replace("{", "").Replace("}", "").Replace("id =", "");
                var empresa =
                (Empresa)EmpresaRepository.PesquisarPeloId(Guid.Parse(idempresa));
                if (empresa != null)
                {
                    empresa.Bloqueado = "Y";
                    EmpresaRepository.Salvar(empresa);
                    Context.SaveChanges();
                }
                return this.Json(empresa.NomeFantasia, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public JsonResult DesbloquearEmpresa(string Id)
        {
            try
            {
                var idempresa = Id.Replace("{", "").Replace("}", "").Replace("id =", "");

                var empresa =
                (Empresa)EmpresaRepository.PesquisarPeloId(Guid.Parse(idempresa));
                if (empresa != null)
                {
                    empresa.Bloqueado = "N";
                    EmpresaRepository.Salvar(empresa);
                    Context.SaveChanges();
                }
                return this.Json(empresa.NomeFantasia, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public override ActionResult Visualizar(Guid Id)
        {
            try
            {
                var uf = new UF();
                ViewBag.ListagemdeUF = uf.Listar().ToList().Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Valor.ToString() });
                TempData["Mensagem"] = "Funcionário editar com sucesso!";
                return base.Visualizar(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}