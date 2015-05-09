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
using System.Web.Helpers;
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

            var _Funcionario = (Funcionario)System.Web.HttpContext.Current.Session["Funcionario"];
            ViewBag.EmpresaLogada = _Funcionario.Empresa.NomeFantasia;


            return View("Index", lista);
        }

        public override ActionResult Visualizar(Guid Id)
        {
            var uf = new UF();
            ViewBag.ListagemdeUF = uf.Listar().ToList().Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Valor.ToString() });

            return base.Visualizar(Id);
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

        public JsonResult ValidaDados(string Id)
        {
            //string campo = string.Empty;
            //Empresa retorno = new Empresa();                   

            //try
            //{
            //    var idempresa = Id.Replace("{", "").Replace("}", "").Replace("id =", "");

            //    var empresa =
            //    (Empresa)EmpresaRepository.PesquisarPeloId(Guid.Parse(idempresa));
            //    if (empresa != null)
            //    {
            //        if (empresa.Cnpj == null)
            //        {
            //            empresaValida = false;
            //            campo = "CNPJ";
            //        }
            //        else if (empresa.NomeFantasia == null)
            //        {
            //            empresaValida = false;
            //            campo = "CNPJ";

            //        }
            //        else if (empresa.RazaoSocial == null)
            //        {
            //            empresaValida = false;
            //            campo = "CNPJ";

            //        }

                    
            //    }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            
        }
    }
}