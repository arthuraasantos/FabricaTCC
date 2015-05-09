using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using FrontEnd.Models.Conversores;
using Infraestrutura;
using Seedwork.Const;
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

        private IEnumerable<SelectListItem> ListaPerfis;
        private IEnumerable<SelectListItem> ListaEmpresas;

        public FuncionarioController(MyContext context, IFuncionarioRepository funcionarioRepository, IPerfilDeAcessoRepository perfildeacessoRepository, IEmpresaRepository empresaRepository)
            : base(context, funcionarioRepository, new FuncionarioToFuncionarioNovo(perfildeacessoRepository, empresaRepository), new FuncionarioToFuncionarioEditar(empresaRepository, perfildeacessoRepository))
        {
            PerfildeacessoRepository = perfildeacessoRepository;
            EmpresaRepository = empresaRepository;
            FuncionarioRepository = funcionarioRepository;

            // Pega a lista de Perfis de Acesso por permissao
            switch (Sessao.PerfilFuncionarioLogado)
            {

                case PerfilAcesso.Administrador: // Traz todos os Perfis
                    ListaPerfis = PerfildeacessoRepository
                                    .Listar()
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                    break;
                case PerfilAcesso.Gerente: // Traz os Perfis Gerente e Funcionario
                    ListaPerfis = PerfildeacessoRepository
                                    .Listar()
                                    .Where(p => p.Descricao != "Administrador")
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                    break;
                case PerfilAcesso.Funcionario: // Traz somente o Perfil Funcionario
                    ListaPerfis = PerfildeacessoRepository
                                    .Listar()
                                    .Where(p => p.Descricao == "FuncionarioComum")
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                    break;
            }


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


        public override ActionResult Visualizar(Guid Id)
        {
            var uf = new UF();

            ViewBag.ListagemdeEmpresas = ListaEmpresas;
            ViewBag.ListagemdePerfis = ListaPerfis;
            ViewBag.ListagemdeUF = uf.Listar().ToList().Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Valor.ToString() });


            var entidade =
                 Repository.PesquisarPeloId(Id);

            var modelEditar = ConversorEdit.Converter(entidade);

            return View("Visualizar", modelEditar);

        }

        public override ActionResult Novo()
        {
            var novo = new FuncionarioNovo()
            {
                Empresas = ListaEmpresas,
                PerfisDeAcesso = ListaPerfis
            };

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
            List<Funcionario> lista = new List<Funcionario>();

            switch (Sessao.PerfilFuncionarioLogado)
            {
                case PerfilAcesso.Gerente: // Se for administrador do sistema, mostrar todas os funcionários da mesma empresa
                    lista = FuncionarioRepository.Listar().Where(f => f.Empresa.Id == Sessao.EmpresaLogada.Id).ToList();
                    break;
                case PerfilAcesso.Funcionario: // Se for administrador do sistema, mostrar somente o funcionário logado
                    lista = FuncionarioRepository.Listar().Where(f => f.Id == Sessao.FuncionarioLogado.Id).ToList();
                    break;
                case PerfilAcesso.Administrador: // Se for administrador do sistema, mostrar todas os funcionários
                    lista = FuncionarioRepository.Listar().ToList();
                    break;
            }

            return View("Index", lista);
        }

        [HttpGet]
        public JsonResult BloquearFuncionario(string Id)
        {
            try
            {
                var idfuncionario = Id.Replace("{", "").Replace("}", "").Replace("id =", "");
                var funcionario =
                (Funcionario)FuncionarioRepository.PesquisarPeloId(Guid.Parse(idfuncionario));
                if (funcionario != null)
                {
                    funcionario.Bloqueado = "Y";
                    FuncionarioRepository.Salvar(funcionario);
                    Context.SaveChanges();
                }
                return this.Json(funcionario.Nome, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult DesbloquearFuncionario(string Id)
        {
            try
            {
                var idfuncionario = Id.Replace("{", "").Replace("}", "").Replace("id =", "");
                var funcionario =
                (Funcionario)FuncionarioRepository.PesquisarPeloId(Guid.Parse(idfuncionario));
                if (funcionario != null)
                {
                    funcionario.Bloqueado = "N";
                    FuncionarioRepository.Salvar(funcionario);
                    Context.SaveChanges();
                }
                return this.Json(funcionario.Nome, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}