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

namespace FrontEnd.Models
{
    //[Authorize]
    public class HomeController : Controller
    {

        private MyContext Context { get; set; }
        private IFuncionarioRepository FuncionarioRepository { get; set; }
        private IPontoEletronicoService PontoService { get; set; }
        private IPontoRepository PontoRepository { get; set; }
        private IFolgaRepository FolgaRepository { get; set; }
        private IFeriasRepository FeriasRepository { get; set; }



        public HomeController(MyContext context, IFuncionarioRepository funcionarioRepository, IPontoEletronicoService pontoService, IPontoRepository pontoRepository, IFolgaRepository folgaRepository, IFeriasRepository feriasRepository)
        {
            PontoRepository = pontoRepository;
            FuncionarioRepository = funcionarioRepository;
            PontoService = pontoService;
            Context = context;
            FolgaRepository = folgaRepository;
            FeriasRepository = feriasRepository;
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
                    
                    
                    DateTime Hoje = DateTime.Now;
                    ViewBag.Aviso = "";
                    var FeriasFuncionario = FeriasRepository.Listar().Where(p => p.Inicio <= Hoje.Date && p.Fim >= Hoje.Date && p.Funcionario.Id == Sessao.FuncionarioLogado.Id && p.Resposta == RespostaSolicitacao.Aprovado).ToList().FirstOrDefault();
                    if (FeriasFuncionario != null)
                    {
                        ViewBag.Aviso = "Este funcionário está está em período de férias entre " + FeriasFuncionario.Inicio.Date.ToString("dd/MM/yyyy") + " e " + FeriasFuncionario.Fim.Date.ToString("dd/MM/yyyy");
                    } else 
                    {
                        var FolgasFuncionario = FolgaRepository.Listar().Where(p => p.Data == Hoje.Date && p.Funcionario.Id == Sessao.FuncionarioLogado.Id && p.Resposta == RespostaSolicitacao.Aprovado).ToList().FirstOrDefault();
                        if (FolgasFuncionario != null)
                        {
                            ViewBag.Aviso = ViewBag.Aviso + "Este funcionário está cumprindo folga hoje";
                        }
                    }


                    if (Sessao.PerfilFuncionarioLogado == PerfilAcesso.Gerente)
                    {
                        ViewBag.QtdePendentesHoras = Context.Set<Solicitacao>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).Count();
                        ViewBag.QtdePendentesFolgas = Context.Set<Folga>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).Count();
                        ViewBag.QtdePendentesFerias = Context.Set<Ferias>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).Count();

                        ViewBag.QtdeRespostasHoras = Context.Set<Solicitacao>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).Count();
                        ViewBag.QtdeRespostasFolgas = Context.Set<Folga>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).Count();
                        ViewBag.QtdeRespostasFerias = Context.Set<Ferias>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).Count();
                    }
                    else
                    {
                        ViewBag.QtdePendentesHoras = Context.Set<Solicitacao>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Id == Sessao.FuncionarioLogado.Id).Count();
                        ViewBag.QtdePendentesFolgas = Context.Set<Folga>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Id == Sessao.FuncionarioLogado.Id).Count();
                        ViewBag.QtdePendentesFerias = Context.Set<Ferias>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Id == Sessao.FuncionarioLogado.Id).Count();

                        ViewBag.QtdeRespostasHoras = Context.Set<Solicitacao>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Id == Sessao.FuncionarioLogado.Id).Count();
                        ViewBag.QtdeRespostasFolgas = Context.Set<Folga>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Id == Sessao.FuncionarioLogado.Id).Count();
                        ViewBag.QtdeRespostasFerias = Context.Set<Ferias>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Id == Sessao.FuncionarioLogado.Id).Count();
                    }
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
            var funcionario = FuncionarioRepository.ListarComPerfil(Sessao.FuncionarioLogado.PerfilDeAcesso).SingleOrDefault(p => p.Email == marcarPonto.Email && p.Senha == marcarPonto.Senha);
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