using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using Infraestrutura;
using Seedwork.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class SolicitacaoController : Controller
    {
        // GET: Solicitacao
        private MyContext Context { get; set; }
        private IPontoRepository PontoRepository { get; set; }
        private ISolicitacaoRepository SolicitacaoRepository { get; set; }

        public SolicitacaoController(MyContext context, IPontoRepository pontoRepository)
        {
            Context = context;
            PontoRepository = pontoRepository;
        }

        public ActionResult Index()
        {
            var lista = SolicitacaoRepository.
                            Listar().
                            ToList().
                            Where(p => p.Funcionario.Empresa == Sessao.EmpresaLogada).
                            Where(p => p.Resposta == RespostaSolicitacao.Nenhuma).
                            OrderBy(p => p.DataHora).
                            ToList();

            ViewBag.ListaAjustes = lista.Where(p => p.Tipo == TipoSolicitacao.Ajuste).ToList();
            ViewBag.ListaInclusoes = lista.Where(p => p.Tipo == TipoSolicitacao.Inclusao).ToList();
            ViewBag.ListaDesconsideracoes = lista.Where(p => p.Tipo == TipoSolicitacao.Desconsideracao).ToList();

            return View(lista);
        }

        public ActionResult Solicitar(DateTime data, Funcionario funcionario)
        {
            //Pega lista para carregar no ponto
            var _ListaPontos = PontoRepository.
                                Listar().
                                ToList().
                                Where(p => p.DataValida.Date == data.Date).
                                Where(p => p.Funcionario.Id == funcionario.Id).
                                Where(p => p.Contabilizar == true).
                                OrderBy(p => p.DataValida).
                                Select(p => new SelectListItem
                                {
                                    Value = p.Id.ToString(),
                                    Text = p.DataValida.ToString("HH:mm")
                                }).
                                ToList();

            ViewBag.ListaBatidas = _ListaPontos;


            Solicitacao item = new Solicitacao()
            {
                DataHora = data,
                Funcionario = Sessao.FuncionarioLogado
            };

            return View(item);
        }


        public ActionResult AprovarRejeitarSolicitacao(Guid Id, RespostaSolicitacao Respt)
        {

            var Solicitacao = SolicitacaoRepository.PesquisarPeloId(Id);
            var Ponto = Solicitacao.Ponto;

            switch (Solicitacao.Tipo)
            {

                case TipoSolicitacao.Ajuste:
                    if (Respt == RespostaSolicitacao.Aprovado)
                    {
                        Ponto.DataValida = Solicitacao.DataHora;
                        PontoRepository.Salvar(Ponto);
                    }
                    break;

                case TipoSolicitacao.Inclusao:
                    if (Respt == RespostaSolicitacao.Aprovado)
                    {
                        Ponto.DataValida = Solicitacao.DataHora;
                        Ponto.Funcionario = Solicitacao.Funcionario;
                        Ponto.Contabilizar = true;
                        PontoRepository.Salvar(Ponto);
                    }
                    break;

                case TipoSolicitacao.Desconsideracao:
                    if (Respt == RespostaSolicitacao.Aprovado)
                    {
                        Ponto.Contabilizar = false;
                        PontoRepository.Salvar(Ponto);
                    }
                    break;

            }

            Solicitacao.Resposta = Respt;

            SolicitacaoRepository.Salvar(Solicitacao);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}