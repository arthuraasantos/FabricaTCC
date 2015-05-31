using System.Web.Mvc;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System.Linq;
using FrontEnd.Models;
using Dominio.Services;
using System;
using Dominio.Repository;
using Dominio.Model;
using Seedwork.Const;

namespace FrontEnd.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {

        private MyContext Context { get; set; }
        private IFuncionarioRepository FuncionarioRepository { get; set; }
        private IPontoEletronicoService PontoService { get; set; }
        private IPontoRepository PontoRepository { get; set; }

        public HomeController(MyContext context, IFuncionarioRepository funcionarioRepository, IPontoEletronicoService pontoService, IPontoRepository pontoRepository)
        {
            PontoRepository = pontoRepository;
            FuncionarioRepository = funcionarioRepository;
            PontoService = pontoService;
            Context = context;
        }

        public ActionResult Index()
        {

            if (Sessao.FuncionarioLogado != null)
            {

                ViewBag.EmailFuncionario = Sessao.FuncionarioLogado.Email;
                ViewBag.Funcionario = Sessao.FuncionarioLogado.Nome;
                ViewBag.Empresa = Sessao.EmpresaLogada.NomeFantasia;

                if (Sessao.PerfilFuncionarioLogado != PerfilAcesso.Administrador)
                {
                    DateTime PrimeiroDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    DateTime UltimoDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                    ViewBag.HorasTrabalhadas = PontoService.QuantidadeDeHorasTrabalhadasPorFuncionario(Sessao.FuncionarioLogado, PrimeiroDiaDoMes, UltimoDiaDoMes).ToString();
                    ViewBag.HorariosMarcadosHoje = PontoService.HorasBatidasPorDiaPorFuncionario(Sessao.FuncionarioLogado, DateTime.Now);

                    ViewBag.QtdeRespostasHoras = Context.Set<Solicitacao>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Id == Sessao.FuncionarioLogado.Id).Count();
                    ViewBag.QtdeRespostasFolgas = Context.Set<Folga>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Id == Sessao.FuncionarioLogado.Id).Count();
                    ViewBag.QtdeRespostasFerias = Context.Set<Ferias>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Id == Sessao.FuncionarioLogado.Id).Count();
                }
                else
                {
                    ViewBag.QtdeEmpresas = Context.Set<Empresa>().Count();
                    ViewBag.QtdeFuncionario = Context.Set<Funcionario>().Count();
                }

                return View();

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult EfetuarMarcacaoDoPonto(PontoMarcar marcarPonto)
        {
            var funcionario = FuncionarioRepository.Listar().SingleOrDefault(p => p.Email == marcarPonto.Email && p.Senha == marcarPonto.Senha);
            if (funcionario == null)
            {
                TempData["Mensagem"] = "Senha incorreta.";
            }
            else
            {
                PontoService.EfetuarMarcacaoDePonto(funcionario);
                TempData["Mensagem"] = "Ponto marcado com sucesso.";
            }
 
            return RedirectToAction("Index");
        }

    }
}