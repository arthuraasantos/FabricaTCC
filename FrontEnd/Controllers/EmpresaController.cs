using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using FrontEnd.Models.Conversores;
using Infraestrutura;
using Infraestrutura.Repositorios;
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
            var funcionario = new Funcionario();
            funcionario = (Funcionario)Session["Funcionario"];
            List<Empresa> lista = new List<Empresa>();

            if (funcionario.PerfilDeAcesso.Descricao == "Gerente/RH")
            {
                ViewBag.Permissao = "GRH";
            }
            else if (funcionario.PerfilDeAcesso.Descricao == "FuncionarioComum")
            {
                ViewBag.Permissao = "FUN";
            }
            else
            {
                ViewBag.Permissao = "ADM";
                lista = EmpresaRepository.Listar().ToList(); // Se for administrador do sistema, mostrar todas as Empresas
            }

            // Se a lista estiver vazia, não é o administrador, então passa a empresa 
            //do funcionario como parametro para listar as empresas
            if (lista.Count == 0)
            {
                lista = EmpresaRepository.Listar().Where(f => f.Id == funcionario.Empresa.Id).ToList();
            }
            return View("Index", lista);
        }

        public override ActionResult Incluir(EmpresaNovo novo)
        {
            var entity = ConversorInsert.Converter(novo);
            entity.Id = Guid.NewGuid();
            entity.Bloqueado = "N";
            Repository.Salvar(entity);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult BloquearEmpresa(string Id)
        {
            var idempresa = Id.Replace("{", "").Replace("}", "").Replace("id =","");
            var empresa = 
            (Empresa)EmpresaRepository.PesquisarPeloId(Guid.Parse(idempresa));
            empresa.Bloqueado = "Y";
            EmpresaRepository.Salvar(empresa);
            Context.SaveChanges();

            return this.Json(empresa.NomeFantasia, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DesbloquearEmpresa(string Id)
        {
            var idempresa = Id.Replace("{", "").Replace("}", "").Replace("id =", "");
         
            var empresa =
            (Empresa)EmpresaRepository.PesquisarPeloId(Guid.Parse(idempresa));
            empresa.Bloqueado = "N";
            EmpresaRepository.Salvar(empresa);
            Context.SaveChanges();

            return this.Json(empresa.NomeFantasia, JsonRequestBehavior.AllowGet);
        }

    }
}