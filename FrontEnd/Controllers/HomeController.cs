using System.Web.Mvc;
using Infraestrutura;
using System.Linq;
using System;
using Dominio.Repository;
using Dominio.Model;
using Seedwork.Const;
using TCCPontoEletronico.AppService.Interface;


namespace FrontEnd.Models
{
    //[Authorize]
    public class HomeController : Controller
    {

        private MyContext Context { get; }
        private IFuncionarioRepository FuncionarioRepository { get; set; }
        
        private IPontoRepository PontoRepository { get; set; }
        private IFolgaRepository FolgaRepository { get; set; }
        private IFeriasRepository FeriasRepository { get; set; }

        private readonly IEmployeeService EmployeeService;
        private readonly IVacationService VacationService;
        private readonly IClearanceService ClearanceService;
        private readonly IPontoEletronicoService PointService;
        private readonly ISolicitationService SolicitationService;



        public HomeController(MyContext context, IFuncionarioRepository funcionarioRepository, IPontoEletronicoService pointService, IPontoRepository pontoRepository, IFolgaRepository folgaRepository, IFeriasRepository feriasRepository,
                      IEmployeeService employeeService, IClearanceService clearanceService, IVacationService vacationService, ISolicitationService solicitationService)
        {
            PontoRepository = pontoRepository;
            FuncionarioRepository = funcionarioRepository;
            PointService = pointService;
            Context = context;
            FolgaRepository = folgaRepository;
            FeriasRepository = feriasRepository;
            EmployeeService = employeeService;
            ClearanceService = clearanceService;
            VacationService = vacationService;
            SolicitationService = solicitationService;
        }

        public ActionResult Index()
        {
            if (EmployeeService.GetEmployeeLogged() != null)
            {
                if (EmployeeService.GetAccessProfile() != PerfilAcesso.Administrador)
                {
                    if (EmployeeService.GetAccessProfile() == PerfilAcesso.Gerente)
                    {
                        //ViewBag.QtdePendentesHoras =  Context.Set<Solicitacao>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).Count();
                        //ViewBag.QtdePendentesFolgas = Context.Set<Folga>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).Count();

                        //ViewBag.QtdePendentesFerias = Context.Set<Ferias>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).Count();

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
                PointService.EfetuarMarcacaoDePonto(funcionario);
                TempData["Mensagem"] = "Ponto marcado com sucesso.";
            }
 
            return RedirectToAction("Index");
        }

        public JsonResult GetEmailEmployee()
        {
            var response = new DefaultJsonResponse();
            try
            {
                response.Message = EmployeeService.GetEmailEmployeeLogged();
                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao buscar o e-mail do funcionário logado.";
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccessProfileDescription()
        {
            var response = new DefaultJsonResponse();
            try
            {
                response.Message = EmployeeService.GetAccessProfileDescription();
                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao buscar o Perfil de Acesso do funcionário logado.";
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNameEmployee()
        {
            var response = new DefaultJsonResponse();
            try
            {
                response.Message = EmployeeService.GetNameEmployeeLogged();
                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao buscar o Nome do funcionário logado.";
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrganizationName()
        {
            var response = new DefaultJsonResponse();
            try
            {
                response.Message = EmployeeService.GetOrganizationNameLogged();
                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao buscar o Nome da empresa do funcionário logado.";
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeWorkHour()
        {
            var response = new DefaultJsonResponse();
            try
            {
                DateTime PrimeiroDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime UltimoDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                response.Message =
                    PointService.QuantidadeDeHorasTrabalhadasPorFuncionario(EmployeeService.GetEmployeeLogged(), PrimeiroDiaDoMes, UltimoDiaDoMes).ToString();
                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao buscar a quantidade de horas trabalhadas.";
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
            }

            return Json(response, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetHitHourForDay()
        {
            var response = new DefaultJsonResponse();
            try
            {
                response.Message = PointService.HorasBatidasPorDiaPorFuncionario(EmployeeService.GetEmployeeLogged(), DateTime.Now);
                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao buscar o número de batidas por dia";
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNotificationWarning()
        {
            //TODO montar lista de retornos na tela para o usuário, o tipo de resposta em Json já retorna uma lista de string
            
            var response = new NotificationJsonResponse();
            DateTime Hoje = DateTime.Now;
            try
            {
                var vacation = VacationService.GetVacationNotificationWarning(Hoje,EmployeeService.GetEmployeeLogged());

                if (vacation != null)
                    response.Message = "Este funcionário está em período de férias entre " + vacation.Inicio.Date.ToString("dd/MM/yyyy") + " e " + vacation.Fim.Date.ToString("dd/MM/yyyy");
                else
                {
                    var clearance = ClearanceService.GetClearanceNotificationWarning(Hoje,EmployeeService.GetEmployeeLogged());
                    if (clearance != null)
                    {
                        response.Message = "Este funcionário está cumprindo folga hoje";
                    }
                }

                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro os avisos do funcionário";
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult GetCountPendingHours()
        {
            var response = new DefaultJsonResponse();
            try
            {
                response.Message = SolicitationService.GetCountPendingHours(EmployeeService.GetEmployeeLogged().Empresa.Id).ToString();
                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao buscar a quantidade de horas pendentes.";
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
            }

            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetCountPendingClearance()
        {
            var response = new DefaultJsonResponse();
            try
            {
                response.Message = ClearanceService.GetCountPendingClearance().ToString();
                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao buscar a quantidade de folgas pendentes.";
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCountPendingVacation()
        {
            var response = new DefaultJsonResponse();
            try
            {
                response.Message = VacationService.GetCountVacationPending().ToString();
                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao buscar a quantidade de férias pendentes.";
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}