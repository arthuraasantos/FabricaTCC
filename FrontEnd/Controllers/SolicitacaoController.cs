using Dominio.Repository;
using Infraestrutura;
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

        //MyContext Context;
        //private IPontoRepository PontoRepository { get; set; }
        //private ISolicitacaoRepository SolicitacaoRepository { get; set; }

        //public SolicitacaoController(
        //    MyContext context, 
        //    IPontoRepository pontoRepository, 
        //    IPontoEletronicoService pontoEletronicoService)

        //    : base(context, pontoRepository, null, new PontoToPontoAjustar())
        //{
        //    Context = context;
        //    PontoRepository = pontoRepository;
        //    PontoEletronicoService = pontoEletronicoService;
        //    FuncionarioRepository = new FuncionarioRepository(context);
        //}
        //
        // TODO : Refazer o Construtor da SolicitacaoController

        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult ListaSolicitacoes()
        //{
        //    var lista = PontoRepository.
        //                    Listar().
        //                    ToList().
        //                    Where(p => p.Status == StatusPonto.Nenhum).
        //                    Where(p => p.DataAjuste != null).
        //                    OrderBy(p => p.DataAjuste).
        //                    ToList();
        //    return View(lista);

        //}
        //public ActionResult AprovarRejeitarAjuste(Guid Id)
        //{

        //    var Ponto = Repository.PesquisarPeloId(Id);

        //    Ponto.Status = Status;
        //    if (Status == StatusPonto.Ajustado)
        //    {
        //        Ponto.DataValida = Ponto.DataAjuste.GetValueOrDefault();
        //    }

        //    Repository.Salvar(Ponto);
        //    Context.SaveChanges();

        //    return RedirectToAction("ListaAprovar");
        //}
        //public ActionResult AprovarRejeitarInclusao(Guid Id)
        //{

        //    var Ponto = Repository.PesquisarPeloId(Id);

        //    Ponto.Status = Status;
        //    if (Status == StatusPonto.Incluido)
        //    {
        //        Ponto.DataValida = Ponto.DataAjuste.GetValueOrDefault();
        //    }

        //    Repository.Salvar(Ponto);
        //    Context.SaveChanges();

        //    return RedirectToAction("ListaAprovar");
        //}
        //public ActionResult AprovarRejeitarDesconsideracao(Guid Id)
        //{

        //    var Ponto = Repository.PesquisarPeloId(Id);

        //    Ponto.Status = Status;

        //    Repository.Salvar(Ponto);
        //    Context.SaveChanges();

        //    return RedirectToAction("ListaAprovar");
        //}

    }
}