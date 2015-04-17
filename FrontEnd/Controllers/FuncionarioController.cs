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
        public IFuncionarioRepository FuncionarioRepository;

        public FuncionarioController(MyContext context, IFuncionarioRepository funcionarioRepository, IPerfilDeAcessoRepository perfildeacessoRepository, IEmpresaRepository empresaRepository)
            : base(context, funcionarioRepository, new FuncionarioToFuncionarioNovo(perfildeacessoRepository, empresaRepository), new FuncionarioToFuncionarioEditar(empresaRepository, perfildeacessoRepository))
        {
            PerfildeacessoRepository = perfildeacessoRepository;
            EmpresaRepository = empresaRepository;
            FuncionarioRepository = funcionarioRepository;
            
        }

        public override ActionResult Visualizar(Guid Id)
        {
            var uf = new UF();
            var funcionario = (Funcionario)Session["Funcionario"];

            ViewBag.ListagemdeEmpresas = EmpresaRepository.Listar().Where(e => e.Id == funcionario.Empresa.Id).ToList().Select(p => new SelectListItem() { Text = p.RazaoSocial, Value = p.Id.ToString() });
            ViewBag.ListagemdePerfis = PerfildeacessoRepository.Listar().Where(p => p.Descricao != "Administrador").ToList().Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
            ViewBag.ListagemdeUF = uf.Listar().ToList().Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Valor.ToString() });

            return base.Visualizar(Id);
        }

        public override ActionResult Novo()
        {
            var novo = new FuncionarioNovo();

            var funcionario = new Funcionario();
            funcionario = (Funcionario)Session["Funcionario"];

            novo.Empresas =
                EmpresaRepository
                    .Listar()
                    .Where(e => e.Id == funcionario.Empresa.Id)
                    .ToList()
                    .Select(p => new SelectListItem() { Text = p.NomeFantasia, Value = p.Id.ToString() });

              novo.PerfisDeAcesso = PerfildeacessoRepository
                .Listar()
                .Where(p => p.Descricao != "Administrador")
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

        public override ActionResult Index()
        {
            var funcionario = new Funcionario();
            funcionario = (Funcionario)Session["Funcionario"];
            List<Funcionario> lista = new List<Funcionario>();

            if (funcionario.PerfilDeAcesso.Descricao == "Gerente/RH")
            {
                ViewBag.Permissao = "GRH";
                lista = FuncionarioRepository.Listar().Where(f => f.Empresa.Id == funcionario.Empresa.Id).ToList();

            }
            else if (funcionario.PerfilDeAcesso.Descricao == "FuncionarioComum")
            {
                ViewBag.Permissao = "FUN";
                lista = FuncionarioRepository.Listar().Where(f => f.Id == funcionario.Id).ToList();

            }
            else
            {
                ViewBag.Permissao = "ADM";
                lista = FuncionarioRepository.Listar().ToList(); // Se for administrador do sistema, mostrar todas os funcionários
            }

            return View("Index", lista);
        }
  
    }
}