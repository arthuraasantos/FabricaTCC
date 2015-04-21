using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using FrontEnd.Models.Conversores;
using Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Seedwork.Const;

namespace FrontEnd.Controllers
{
    public class SolicitacaoController : BaseController<Solicitacao, SolicitacaoCriar, SolicitacaoAprovar>
    {
        private IPontoRepository PontoRepository { get; set; }
        private IFuncionarioRepository FuncionarioRepository { get; set; }


        public SolicitacaoController(MyContext context, ISolicitacaoRepository solicitacaoRepository, IPontoRepository pontoRepository, IFuncionarioRepository funcionarioRepository)
            : base(context, solicitacaoRepository, new SolicitacaoToSolicitacaoCriar(), new SolicitacaoToSolicitacaoAjustar())
        {
            PontoRepository = pontoRepository;
            FuncionarioRepository = funcionarioRepository;
        }

        public override ActionResult Index()
        {
            var _lista = Repository.
                            Listar().
                            ToList().
                            Where(p => p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).
                            Where(p => p.Resposta == RespostaSolicitacao.Nenhuma).
                            OrderBy(p => p.DataHora).
                            ToList();

            return View(_lista);
        }

        public ActionResult Solicitar(DateTime data, string email)
        {
            Funcionario func = new Funcionario();
            func = FuncionarioRepository.PesquisaPeloEmail(email);

            //Pega lista para carregar no ponto
            var _ListaPontos = PontoRepository.
                                Listar().
                                ToList().
                                Where(p => p.DataValida.Date == data.Date).
                                Where(p => p.Funcionario.Id == func.Id).
                                Where(p => p.Contabilizar == true).
                                OrderBy(p => p.DataValida).
                                Select(p => new SelectListItem
                                {
                                    Value = p.Id.ToString(),
                                    Text = p.DataValida.ToString("HH:mm")
                                }).
                                ToList();
            ViewBag.ListaBatidas = _ListaPontos;


            SolicitacaoCriar item = new SolicitacaoCriar()
            {
                Data = data.ToString("dd/MM/yyyy"),
                Hora = data.TimeOfDay.ToString(),
                Funcionario = func.Email,
                Resposta = RespostaSolicitacao.Nenhuma
            };

            return View(item);
        }

        public ActionResult Criar(SolicitacaoCriar SolicitacaoNovo)
        {
            Solicitacao Solicitacao = new Solicitacao();
            ConversorInsert.AplicarValores(SolicitacaoNovo, Solicitacao);

            //Pega o Funcionario pelo Email
            Solicitacao.Funcionario = FuncionarioRepository.PesquisaPeloEmail(SolicitacaoNovo.Funcionario);

            if (SolicitacaoNovo.Tipo != TipoSolicitacao.Inclusao)
            {
                //Pega ponto pelo GUID
                Solicitacao.Ponto = PontoRepository.PesquisarPeloId(SolicitacaoNovo.Ponto);
            }

            Repository.Salvar(Solicitacao);
            Context.SaveChanges();

            return RedirectToAction("Lista", "Ponto");
        }

        public ActionResult AprovarRejeitarSolicitacao(Guid Id, RespostaSolicitacao resposta)
        {

            var Solicitacao = Repository.PesquisarPeloId(Id);
            Ponto Pto = Solicitacao.Ponto;

            Solicitacao.Resposta = resposta;
            if (resposta == RespostaSolicitacao.Aprovado)
            {

                switch (Solicitacao.Tipo)
                {

                    case TipoSolicitacao.Ajuste:
                        if (resposta == RespostaSolicitacao.Aprovado)
                        {
                            Pto.DataValida = Solicitacao.DataHora;
                            PontoRepository.Salvar(Pto);
                        }
                        break;

                    case TipoSolicitacao.Inclusao:
                        if (resposta == RespostaSolicitacao.Aprovado)
                        {
                            Ponto NewPto = new Ponto()
                            {
                                Id = Guid.NewGuid(),
                                DataValida = Solicitacao.DataHora,
                                Funcionario = Solicitacao.Funcionario,
                                Contabilizar = true
                            };

                            PontoRepository.Salvar(NewPto);
                            Solicitacao.Ponto = NewPto;
                        }
                        break;

                    case TipoSolicitacao.Desconsideracao:
                        if (resposta == RespostaSolicitacao.Aprovado)
                        {
                            Pto.Contabilizar = false;
                            PontoRepository.Salvar(Pto);
                        }
                        break;

                }
            }

            Repository.Salvar(Solicitacao);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}