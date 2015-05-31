using Dominio.Model;
using Dominio.Repository;
using Dominio.Services;
using FrontEnd.Models;
using FrontEnd.Models.Conversores;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Seedwork.Entity;
using Seedwork.Const;
using System.Web.UI;
using SeedWork.Tools;

namespace FrontEnd.Controllers
{
    //[Authorize]
    public class PontoController : Controller
    {
        // GET: Ponto
        MyContext Context;
        private IPontoRepository PontoRepository { get; set; }
        private IPontoEletronicoService PontoEletronicoService { get; set; }
        private IFuncionarioRepository FuncionarioRepository { get; set; }

        public PontoController(MyContext context, IPontoRepository pontoRepository, IPontoEletronicoService pontoEletronicoService)
        {
            Context = context;
            PontoRepository = pontoRepository;
            PontoEletronicoService = pontoEletronicoService;
            FuncionarioRepository = new FuncionarioRepository(context);
        }

        public ActionResult Marcar(string email, string senha, string parametro)
        {
            var funcionarioParaMarcar = new Funcionario();
            funcionarioParaMarcar = FuncionarioRepository.PesquisaParaLogin(email, Criptografia.Encrypt(senha));

            if (funcionarioParaMarcar != null)
            {
                PontoEletronicoService.EfetuarMarcacaoDePonto(funcionarioParaMarcar);
                TempData["Mensagem"] = "Marcação efetuada com sucesso!";
            }
            else
            {
                TempData["MensagemAlerta"] = "Senha incorreta";
            }


            if ((parametro != string.Empty) && (parametro != null))
            {
                // Registro de ponto sem Login
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Lista(string Email, DateTime? Data)
        {
            // Pega email, ou default usuario logado
            string _Email = Sessao.FuncionarioLogado.Email;
            if ((Email != null) && (Email != String.Empty)) { _Email = Email; }
            ViewBag.EmailLogado = _Email;

            // Pega a data, ou default data atual
            DateTime _Data = DateTime.Now;
            if (Data != null) { _Data = DateTime.Parse(Data.ToString()); }

            // Recupera a lista de pontos batidos 
            var _ListaCompleta = PontoRepository.
                            Listar().
                            ToList().
                            Where(p => p.DataValida.Month == _Data.Month).
                            Where(p => p.DataValida.Year == _Data.Year).
                            Where(p => p.Funcionario.Email == _Email).
                            OrderBy(p => p.DataValida).
                            ToList();

            DateTime _PrimeiraData = new DateTime(_Data.Year, _Data.Month, 1);
            int _Dias = DateTime.DaysInMonth(_Data.Year, _Data.Month);
            Dictionary<DateTime, List<Ponto>> Dicionario = new Dictionary<DateTime, List<Ponto>>();
            Dictionary<DateTime, TimeSpan> DicionarioHoras = new Dictionary<DateTime, TimeSpan>();

            int Maior = 0;

            for (int i = 0; i < _Dias; i++)
            {

                var _ListaPorDia = _ListaCompleta.Where(p => p.DataValida.Date == _PrimeiraData.AddDays(i).Date).ToList();

                int _QtdeBatidas = _ListaPorDia.Count();
                if (_QtdeBatidas > Maior)
                {
                    Maior = _QtdeBatidas;
                }
                
                Dicionario.Add(_PrimeiraData.AddDays(i), _ListaPorDia);
                DicionarioHoras.Add(_PrimeiraData.AddDays(i), PontoEletronicoService.QuantidadeDeHorasTrabalhadasPorFuncionarioPorDia(Sessao.FuncionarioLogado, _PrimeiraData.AddDays(i)));
            }


            ViewBag.MaiorBatidas = ((int)((Maior + 1) / 2)) * 2;
            ViewBag.DicionarioHoras = DicionarioHoras;

            return View(Dicionario);
        }
        public ActionResult Index()
        {
            return View();
        }


    }
}