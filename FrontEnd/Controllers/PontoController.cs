
using Dominio.Model;
using Dominio.Repository;
using Dominio.Services;
using FrontEnd.Models;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    //[Authorize]
    public class PontoController : Controller
    {
        // GET: Ponto
        MyContext Context;
        private IPontoRepository PontoRepository { get; set; }
        private IPontoEletronicoService PontoEletronicoService { get; set; }
        private FuncionarioRepository FuncionarioRepository { get; set; }

        public PontoController(MyContext context, IPontoRepository pontoRepository, IPontoEletronicoService pontoEletronicoService)
        {
            Context = context;
            PontoRepository = pontoRepository;
            PontoEletronicoService = pontoEletronicoService;
            FuncionarioRepository = new FuncionarioRepository(context);

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Marcar(string email, string senha)
        {

            var funcionarioParaMarcar = new Funcionario();
            funcionarioParaMarcar = FuncionarioRepository.PesquisaParaLogin(email, senha);

            if (funcionarioParaMarcar != null)
            {
                PontoEletronicoService.EfetuarMarcacaoDePonto(funcionarioParaMarcar);
                ViewBag.Mensagem = new Mensagem() { TextoResumido = "Marcação efetuada com sucesso!" };
            }
            else
            {
                ViewBag.Mensagem = new Mensagem() { TextoResumido = "Atenção: Email ou senha" };
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult Consultar(string Email, DateTime? Data)
        {
            DateTime _Data = DateTime.MinValue;
            if (Data != null) { _Data = DateTime.Parse(Data.ToString()); }

            string _Email = String.Empty;
            if (Email != String.Empty) { _Email = Email; }

            var lista = PontoRepository.
                            Listar().
                            ToList().
                            Where(p => p.DataDaMarcacao.Date == _Data.Date ).
                            Where(p => p.Funcionario.Email == _Email).
                            OrderBy(p => p.DataDaMarcacao).
                            ToList();

            return View(lista);
        }
    }
}