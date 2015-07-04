using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using FrontEnd.Models.Conversores;
using Infraestrutura;
using Infraestrutura.Repositorios;
using Seedwork.Const;
using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;

namespace FrontEnd.Models
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
                if (ModelState.IsValid)
                {
                    var entity = ConversorInsert.Converter(novo);
                    entity.Id = Guid.NewGuid();
                    entity.Bloqueado = "N";
                    Repository.Salvar(entity);
                    Context.SaveChanges();
                    //TempData["Mensagem"] = "Empresa cadastrada com sucesso!";
                    TempData["Empresa"] = entity;
                    return RedirectToAction("Novo", "Funcionario");
                }
                else
                {
                    TempData["MensagemErro"] = "Dados inválidos";
                    return View("Novo");
                }

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
                return base.Visualizar(Id);
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = "Erro ao editar empresa. " + e.Message;
                return RedirectToAction("Index");
            }
        }

        public override ActionResult Editar(EmpresaEditar editar)
        {
            try
            {
                var entity = Repository.PesquisarPeloId(editar.Id);
                ConversorEdit.AplicarValores(editar, entity);
                Repository.Salvar(entity);
                Context.SaveChanges();
                TempData["Mensagem"] = "Empresa alterada com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro ao alterar empresa! " + e.Message;
                return RedirectToAction("Index");
            }

        }

        public JsonResult AtualizaDadosEndereco(string cep)
        {
            Tools t = new Tools();
            return this.Json(t.BuscaCep(cep), JsonRequestBehavior.AllowGet);
        }

    }
}