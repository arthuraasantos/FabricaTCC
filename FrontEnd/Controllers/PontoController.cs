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
    public class PontoController : BaseController<Ponto, PontoMarcar, null>
    {
        // GET: Ponto
        //MyContext Context;
        private IPontoRepository PontoRepository { get; set; }
        private IPontoEletronicoService PontoEletronicoService { get; set; }
        private IFuncionarioRepository FuncionarioRepository { get; set; }

        public PontoController(MyContext context, IPontoRepository pontoRepository, IPontoEletronicoService pontoEletronicoService) : base(context, pontoRepository, null, null)
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
                ViewBag.Mensagem = new Mensagem() { TextoResumido = "Marcação efetuada com sucesso!" };
            }
            else
            {
                ViewBag.Mensagem = new Mensagem() { TextoResumido = "Atenção: Email ou senha" };
            }


            if ((parametro != string.Empty) && (parametro != null))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult ListaAjustar(string Email, DateTime? Data)
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
            }


            ViewBag.MaiorBatidas = ((int)((Maior + 1) / 2)) * 2;


            return View(Dicionario);
        }
        public ActionResult Inclusao(DateTime data) {
            return View();
        }
        public ActionResult Desconsiderar(DateTime data)
        {
            var _ListaCompleta = PontoRepository.
                            Listar().
                            ToList().
                            Where(p => p.DataValida.Date == data.Date).
                            OrderBy(p => p.DataValida).
                            Select(p => new SelectListItem
                            {
                                Value = p.Id.ToString(),
                                Text = p.DataValida.ToString("HH:mm")
                            }).
                            ToList();

            ViewBag.ListaBatidas = _ListaCompleta;

            return View();
        }


        public override ActionResult Editar(PontoEditar editar)
        {

            var entity = Repository.PesquisarPeloId(editar.Id);

            ConversorEdit.AplicarValores(editar, entity);

            Repository.Salvar(entity);
            Context.SaveChanges();

            return RedirectToAction("ListaAjustar");
        }



        public override System.Web.Mvc.ActionResult Index()
        {

            return View();
        }


    }
}