using System.Web.Mvc;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System.Linq;
using FrontEnd.Models;
using Dominio.Services;
using System;
using Dominio.Repository;
using Dominio.Model;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {

        private MyContext Context { get; set; }
        private IFuncionarioRepository FuncionarioRepository { get; set; }
        private IPontoEletronicoService PontoService { get; set; }
        private IPontoRepository PontoRepository { get; set; }

        public HomeController(MyContext context, IFuncionarioRepository funcionarioRepository, IPontoEletronicoService pontoService, IPontoRepository pontoRepository) {
            PontoRepository = pontoRepository;
            FuncionarioRepository = funcionarioRepository;
            PontoService = pontoService;
        }

        //[Authorize]
        public ActionResult Index()
        {

            Funcionario funcionario = new Funcionario();
            funcionario.Empresa = new Empresa();

            funcionario = (Funcionario)Session["funcionario"];

            if (funcionario != null)
            {
                ViewBag.NomeFuncionario = funcionario.Nome;
                ViewBag.Empresa = funcionario.Empresa.NomeFantasia;
                ViewBag.HorariosMarcadosHoje = PontoService.HorasBatidasPorDiaPorFuncionario(funcionario, DateTime.Now);
                ViewBag.HorasTrabalhadas = PontoService.QuantidadeDeHorasTrabalhadasPorFuncionario(funcionario, DateTime.Now, DateTime.Now.AddDays(-30));
            }
            else
            {
                ViewBag.NomeFuncionario = "[ Funcionário não definido ]";
                ViewBag.Empresa = "[ Empresa não definida ]";
                ViewBag.HorariosMarcadosHoje = "00:00 - 00:00 - 00:00 - 00:00";
                ViewBag.HorasTrabalhadas = "[ Não foi possível calcular a hora sem um funcionário definido ]";
            }

            return View();

        }

        public ActionResult MarcarPonto()
        {

            ViewBag.ListaDeMarcacoes = PontoRepository.Listar().OrderByDescending(p => p.DataDaMarcacao).ToList();
            ViewBag.Mensagem = new Mensagem();
            return View("View", new MarcarPontoViewModel());
        }

        public ActionResult EfetuarMarcacaoDoPonto(MarcarPontoViewModel marcarPonto)
        {
            var funcionario = FuncionarioRepository.Listar().SingleOrDefault(p => p.Email == marcarPonto.Email && p.Senha == marcarPonto.Senha);
            if (funcionario == null)
            {
                ViewBag.Mensagem = new Mensagem() { TextoResumido = "Funcionario Não Encontrado" };
                return View();
            }

            PontoService.EfetuarMarcacaoDePonto(funcionario);

            ViewBag.Mensagem = new Mensagem() { TextoResumido = "Ponto Marcado Com Sucesso!" };

            return RedirectToAction("MarcarPonto");
        }
 
    }
}