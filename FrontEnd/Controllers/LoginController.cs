
using Dominio.Model;
using Infraestrutura;
using Infraestrutura.Repositorios;
using Seedwork.Const;
using SeedWork.Tools;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace FrontEnd.Models
{
    public class LoginController : Controller
    {

        public readonly MyContext Contexto;
        public FuncionarioRepository EmployeeRepository { get; set; }

        public readonly ILoginService LoginService;


        public LoginController(ILoginService loginService, MyContext context, FuncionarioRepository employeeRepository)
        {
            Contexto = context;
            EmployeeRepository = employeeRepository;
            LoginService = loginService;
        }

        public ActionResult Index()
        {
            string userCookie = GetCookie();

            if (!string.IsNullOrWhiteSpace(userCookie) && Session["Funcionario"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Autenticar(string email, string password, string remember)
        {
            var response = new DefaultJsonResponse();

            try
            {
                var funcionarioParaLogin = new Funcionario();
                funcionarioParaLogin =
                    EmployeeRepository.
                    PesquisaParaLogin(
                        email,
                        Criptografia.Encrypt(password));

                if (funcionarioParaLogin != null)
                {
                    //if (funcionarioParaLogin.PerfilDeAcesso.Descricao == Seedwork.Const.PerfilAcesso.Administrador.ToString())
                    //{
                    //    TempData["MensagemAlerta"] = "O administrador do sistema está bloqueado! Entre em contato com os responsáveis pelo sistema!";
                    //}
                    //else
                    if (funcionarioParaLogin.Bloqueado == "Y")
                    {
                        response.Message = "Este funcionário está bloqueado! Entre em contato com o Gerente!";
                    }
                    else
                    {
                        if (funcionarioParaLogin.Empresa.Bloqueado == "Y")
                        {
                            response.Message = "A empresa deste funcionário está bloqueada! Entre em contato com o Administrador do sistema!";
                        }
                        else
                        {
                            if (remember == "on")
                                CreateLoginCookie(email);

                            Session.Add("Funcionario", funcionarioParaLogin);

                            // Direciona para a página HOME.
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    response.IsValid = false;
                    response.Message = "Usuário ou senha incorreta";
                    response.TypeResponse = TypeResponse.Error;
                }

                return this.Json(response, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                response.Message = "Erro de auteticação" + ex.Message;
                return this.Json(response, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult Logout()
        {
            //Destruir o Ticket de acesso do usuario...
            FormsAuthentication.SignOut(); //remove a permissão...

            //Destruir os dados da Session
            Session.Remove("Funcionario");
            Session.Abandon();

            // Remove Cookie se existente
            DestroyCookie();

            //redirecionar para a página de Login
            return RedirectToAction("Index", "Login");
        }

        public bool CreateLoginCookie(string email)
        {
            try
            {
                HttpCookie myCookie = new HttpCookie("pontoeletronico");
                myCookie["Email"] = email;
                myCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(myCookie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetCookie()
        {
            var cookie = Request.Cookies["pontoeletronico"];

            if (cookie != null)
                return cookie["Email"];
            else
                return string.Empty;
        }

        public void DestroyCookie()
        {
            HttpCookie cookie = Request.Cookies["pontoeletronico"];
            cookie.Expires = DateTime.Now.AddDays(-2);
            Response.Cookies.Add(cookie);
        }

        [HttpGet]
        public JsonResult LoginValidate(string email, string password, string remember)
        {
            // Método para validar o login do sistema
            var response = new DefaultJsonResponse();

            string errorMessage = string.Empty;

            try
            {
                var invalidMessage = LoginService.IsValid(email, password);

                if (!string.IsNullOrWhiteSpace(invalidMessage))
                {
                    response.IsValid = false;
                    response.TypeResponse = TypeResponse.Error;
                    response.Message = invalidMessage;
                }

            }
            catch (Exception ex)
            {
                //TODO implementar log de erro e enviar e-mail para nós 3(Arthur,Marlon e Charles)
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
                response.Message = "Erro ao validar o login" + ex.Message;
            }

            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cria um novo usuário do sistema(Nova empresa, novo perfil e gerente)
        /// </summary>
        /// <returns></returns>
        public JsonResult Register(string fantasyName, string employeeName, string employeeCpf, string employeeEmail)
        {
            var response = new DefaultJsonResponse();

            try
            {
                NewRegisterDTO registerDto = new NewRegisterDTO(fantasyName, employeeName, employeeCpf, employeeEmail);

                LoginService.NewLogin(registerDto);
                
                response.IsValid = true;
                response.TypeResponse = TypeResponse.Success;

                var split = employeeName.Split(' ');
                
                response.Message = "Sucesso, " + split[0]+ ". Enviamos seus dados de acesso por e-mail.";
            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.TypeResponse = TypeResponse.Error;
                response.Message = "Erro ao criar novo usuário" + ex.Message;
            }

            return this.Json(response, JsonRequestBehavior.AllowGet);

        }
        public ActionResult NewRegister()
        {
            return View();
        }
    }
}