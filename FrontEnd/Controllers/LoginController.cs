
using Dominio.Model;
using FrontEnd.Models;
using Infraestrutura;
using Infraestrutura.Repositorios;
using SeedWork.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace FrontEnd.Models
{
    public class LoginController : Controller
    {

        public MyContext Contexto { get; set; }
        public FuncionarioRepository FuncionarioRepository { get; set; }


        public LoginController()
        {
            Contexto = new MyContext();
            FuncionarioRepository = new FuncionarioRepository(Contexto);
        }


        public ActionResult Index()
        {
            string employeeEmail = GetCookie();
            Funcionario employee = new Funcionario();

            // TODO Retirar Session
            //if(employeeEmail.Equals(string.Empty) )
            employee = (Funcionario)Session["Funcionario"];

            if (employee != null)
                FormsAuthentication.SetAuthCookie(employee.Email, true);

            if (employee != null)
            {
                ViewBag.Funcionario = FuncionarioRepository.Listar().Where(f => f.Email == employee.Email);
                return RedirectToAction("Index", "Home");
            }

            return View();

        }
        [HttpPost]
        public ActionResult Autenticar(FuncionarioLogin model, string remember)
        {
            try
            {
                var funcionarioParaLogin = new Funcionario();
                funcionarioParaLogin =
                    FuncionarioRepository.
                    PesquisaParaLogin(
                        model.Email,
                        Criptografia.Encrypt(model.Senha));

                if (funcionarioParaLogin != null)
                {
                    //if (funcionarioParaLogin.PerfilDeAcesso.Descricao == Seedwork.Const.PerfilAcesso.Administrador.ToString())
                    //{
                    //    TempData["MensagemAlerta"] = "O administrador do sistema está bloqueado! Entre em contato com os responsáveis pelo sistema!";
                    //}
                    //else
                    if (funcionarioParaLogin.Bloqueado == "Y")
                    {
                        TempData["MensagemAlerta"] = "Este funcionário está bloqueado! Entre em contato com o Gerente!";
                    }
                    else
                    {
                        if (funcionarioParaLogin.Empresa.Bloqueado == "Y")
                        {
                            TempData["MensagemAlerta"] = "A empresa deste funcionário está bloqueada! Entre em contato com o Administrador do sistema!";
                        }
                        else
                        {
                            if (remember == "on")
                                CreateLoginCookie(model.Email);

                            Session.Add("Funcionario", funcionarioParaLogin);

                            //Redireciona para a mesma view e o tratamento do que vai aparecer será nas views.
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    TempData["MensagemAlerta"] = "Usuário ou senha incorreta!";
                }

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro de auteticação." + e.Message;
            }

            return RedirectToAction("Index", "Login");
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

        public JsonResult LoginValidate(FuncionarioLogin model)
        {
            // Método para validar o login do sistema
            string errorMessage = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(model.Email))
                    errorMessage = "Preencha o e-mail";
                else if (string.IsNullOrWhiteSpace(model.Senha))
                    errorMessage = "Preencha o campo senha";

                if (string.IsNullOrEmpty(errorMessage))
                {
                    return Json(new
                    {
                        IsValid = true,
                        Message = errorMessage
                    });
                }

                return Json(new
                {
                    IsValid = false,
                    Message = errorMessage
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    IsValid = false,
                    Message = ex.Message
                });
            }




        }
    }
}