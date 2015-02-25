using System.Web.Mvc;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System.Linq;
using FrontEnd.Models;
using Dominio.Services;
using System;
using Dominio.Repository;

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

        public ActionResult Index()
        {

            var listaDeFuncionarios = FuncionarioRepository
                                            .Listar()
                                            .ToList()
                                            .Select(p => new FuncionarioComHorasTrabalhadas() {
                                                Nome =p.Nome,
                                                Email = p.Email,
                                                Perfil = p.PerfilDeAcesso.Descricao,
                                                HorasTrabalhadas = PontoService.QuantidadeDeHorasTrabalhadasPorFuncionario(p, DateTime.Now)
                                            })
                                            .ToList();

            return View(listaDeFuncionarios);
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