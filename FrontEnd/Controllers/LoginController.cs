
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
            var funcionario = (Funcionario)Session["Funcionario"];
            if (funcionario != null)
            {

                ViewBag.Funcionario = FuncionarioRepository.Listar().Where(f => f.Email == funcionario.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Autenticar(FuncionarioLogin model, string manterLogado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    if (model.Email.ToString() == String.Empty) { TempData["MensagemAlerta"] = "Informe o login!"; }
                    if (model.Senha.ToString() == String.Empty) { TempData["MensagemAlerta"] = "Informe a senha!"; }
                }
                else
                {
                    var funcionarioParaLogin = new Funcionario();
                    funcionarioParaLogin = 
                        FuncionarioRepository.
                        PesquisaParaLogin(
                            model.Email, 
                            Criptografia.Encrypt(model.Senha));

                    if (funcionarioParaLogin != null)
                    {
                        if (funcionarioParaLogin.Bloqueado == "Y") {
                            TempData["MensagemAlerta"] = "Este funcionário está bloqueado! Entre em contato com o Gerente!";
                        } else {
                            if (funcionarioParaLogin.Empresa.Bloqueado == "Y") {
                                TempData["MensagemAlerta"] = "A empresa deste funcionário está bloqueada! Entre em contato com o Administrador do sistema!";
                            } else {
                                if (manterLogado == "S")
                                {
                                    Session.Add("Dados", "manterLogado");
                                }
                                
                                FormsAuthentication.SetAuthCookie(model.Email, false);
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
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro de auteticação." + e.Message;
            }

            return RedirectToAction("Index","Login");
        }

        public ActionResult Logout()
        {
            //Destruir o Ticket de acesso do usuario...
            FormsAuthentication.SignOut(); //remove a permissão...
            //Destruir os dados da Session
            Session.Remove("Funcionario");
            Session.Abandon();

            //redirecionar para a página de Login
            return RedirectToAction("Index", "Login");
        }
    }
}