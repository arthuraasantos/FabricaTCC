using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using FrontEnd.Models.Conversores;
using Infraestrutura;
using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace FrontEnd.Controllers
{
    
    public class FuncionarioController : BaseController<Funcionario, FuncionarioNovo, FuncionarioEditar>
    {
        public IPerfilDeAcessoRepository PerfildeacessoRepository;
        public IEmpresaRepository EmpresaRepository;

        public FuncionarioController(MyContext context, IFuncionarioRepository funcionarioRepository, IPerfilDeAcessoRepository perfildeacessoRepository, IEmpresaRepository empresaRepository)
            : base(context, funcionarioRepository, new FuncionarioToFuncionarioNovo(perfildeacessoRepository, empresaRepository), new FuncionarioToFuncionarioEditar(empresaRepository, perfildeacessoRepository))
        {
            PerfildeacessoRepository = perfildeacessoRepository;
            EmpresaRepository = empresaRepository;
            
        }

        public override ActionResult Visualizar(Guid Id)
        {

            ViewBag.ListagemdeEmpresas = EmpresaRepository.Listar().ToList().Select(p => new SelectListItem() { Text = p.RazaoSocial, Value = p.Id.ToString() });
            ViewBag.ListagemdePerfis = PerfildeacessoRepository.Listar().ToList().Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
            return base.Visualizar(Id);
        }

        public override ActionResult Novo()
        {
            var novo = new FuncionarioNovo();

            novo.Empresas =
                EmpresaRepository
                    .Listar()
                    .ToList()
                    .Select(p => new SelectListItem() { Text = p.NomeFantasia, Value = p.Id.ToString() });

              novo.PerfisDeAcesso = PerfildeacessoRepository
                .Listar()
                .ToList()
                .Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                
            return View("Novo", novo);
        }

        private object PerfilDeAcessoPadrao()
        {
            throw new NotImplementedException();
        }

        public override ActionResult Incluir(FuncionarioNovo novo)
        {
            if (ModelState.IsValid)
            {
                return base.Incluir(novo);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult AtualizaDadosEndereco(string cep)
        {
            Tools t = new Tools();
            return this.Json(t.BuscaCep(cep), JsonRequestBehavior.AllowGet);
        }   

    }
}