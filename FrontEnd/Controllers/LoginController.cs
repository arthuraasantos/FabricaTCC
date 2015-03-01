
using Dominio.Model;
using FrontEnd.Models;
using Infraestrutura;
using Infraestrutura.Repositorios;
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
            return View();
        }

        [HttpPost]
        public ActionResult Autenticar(FuncionarioLogin model)
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
                        FuncionarioRepository.PesquisaParaLogin(model.Email, model.Senha);

                    if (funcionarioParaLogin != null)
                    {
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
    }
}