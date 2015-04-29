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
        }

        public ActionResult Index()
        {

            if (Sessao.FuncionarioLogado != null)
            {
                ViewBag.EmailFuncionario = Sessao.FuncionarioLogado.Email;
                ViewBag.Funcionario = Sessao.FuncionarioLogado.Nome;
                ViewBag.Empresa = Sessao.FuncionarioLogado.Empresa.NomeFantasia;
                ViewBag.HorariosMarcadosHoje = PontoService.HorasBatidasPorDiaPorFuncionario(Sessao.FuncionarioLogado, DateTime.Now);
                ViewBag.HorasTrabalhadas = PontoService.QuantidadeDeHorasTrabalhadasPorFuncionario(Sessao.FuncionarioLogado, DateTime.Now.AddDays(-30), DateTime.Now);

                // Armazena a permissão na tela inicial - pode ser usada onde precisar na página inicial
                switch (Sessao.PerfilFuncionarioLogado)
                {
                    case PerfilAcesso.Gerente:
                        ViewBag.Permissao = "GRH";
                        break;
                    case PerfilAcesso.Funcionario:
                        ViewBag.Permissao = "FUN";
                        break;
                    case PerfilAcesso.Administrador:
                        ViewBag.Permissao = "ADM";
                        break;
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
                ViewBag.Mensagem = new Mensagem() { TextoResumido = "Funcionario Não Encontrado" };
                return View();
            }

            PontoService.EfetuarMarcacaoDePonto(funcionario);

            ViewBag.Mensagem = new Mensagem() { TextoResumido = "Ponto Marcado Com Sucesso!" };

            return RedirectToAction("MarcarPonto");
        }

    }
}