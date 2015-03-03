
using Dominio.Model;
using Dominio.Repository;
using Dominio.Services;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class PontoController : Controller
    {
        // GET: Ponto
        MyContext Context;
        private IPontoRepository PontoRepository {get; set; }
        private IPontoEletronicoService PontoEletronicoService { get; set; }

        public PontoController(MyContext context, IPontoRepository pontoRepository, IPontoEletronicoService pontoEletronicoService)
        {
            Context = context;
            PontoRepository = pontoRepository;
            PontoEletronicoService = pontoEletronicoService;
            
        }
        public ActionResult Index()
        {   
            Funcionario funcionario = new Funcionario();
            funcionario.Empresa = new Empresa();

            funcionario = (Funcionario)Session["funcionario"];

            if (funcionario != null)
            {
                ViewBag.Horarios = PontoEletronicoService.HorasBatidasPorDiaPorFuncionario(funcionario, DateTime.Now);
                ViewBag.Empresa = funcionario.Empresa.NomeFantasia;
            }
            else
            {
                ViewBag.Horarios = "Sem horários";
                ViewBag.Empresa = "-";
            }

            return View();
        }

        public ActionResult Marcar()
        {
            
            return View();
        }
    }
}