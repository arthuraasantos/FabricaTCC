
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


namespace FrontEnd.Controllers
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
            var funcionario = Session["Funcionario"];
            if (funcionario != null)
            {
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
                    throw new Exception();
                }
                else
                {
                    var funcionarioParaLogin = new Funcionario();
                    funcionarioParaLogin = 
                        FuncionarioRepository.PesquisaParaLogin(model.Email, Criptografia.Encrypt(model.Senha));

                    if (funcionarioParaLogin != null)
                    {
                        if (manterLogado == "S")
                        {
                            Session.Add("Dados", "manterLogado");                                

                        }

                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        Session.Add("Funcionario", funcionarioParaLogin);                                

                        //Redireciona para a mesma view e o tratamento do que vai aparecer será nas views.
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Mensagem = "Erro";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = "Erro de auteticação." + e.Message;
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