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
    public class FeriasController : BaseController<Ferias, FeriasCriar, FeriasAprovar>
    {
        private IFuncionarioRepository FuncionarioRepository { get; set; }
        private IFeriasRepository FeriasRepository { get; set; }

        public FeriasController(MyContext context, IFeriasRepository feriasRepository, IFuncionarioRepository funcionarioRepository)
            : base(context, feriasRepository, new FeriasToFeriasCriar(), new FeriasToFeriasAjustar())
        {
            FuncionarioRepository = funcionarioRepository;
            FeriasRepository = feriasRepository;
        }

        public override ActionResult Index()
        {
            
            Funcionario _func = (Funcionario)Session["Funcionario"];
            ViewBag.Email = _func.Email;
           

            
            var _lista = Repository.
                            Listar().
                            ToList().
                            Where(p => p.Resposta == RespostaSolicitacao.Nenhuma).
                            Where(p => p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).
                            OrderBy(p => p.Inicio).
                            ToList();

            return View(_lista);
        }

        public ActionResult Solicitar()
        {
            Funcionario _func = (Funcionario)Session["Funcionario"];
            ViewBag.Email = _func.Email;
            ViewBag.Nome = _func.Nome;
            ViewBag.Empresa = _func.Empresa.NomeFantasia;
            return View();
        }

        public ActionResult SolicitarFerias(DateTime inicio, DateTime fim, string email)
        {
            Funcionario func = new Funcionario();
            func = FuncionarioRepository.PesquisaPeloEmail(email);

            FeriasCriar item = new FeriasCriar()
            {
                Inicio = inicio.Date,
                Fim = fim.Date,
                Funcionario = func.Email,
                Resposta = RespostaSolicitacao.Nenhuma
            };

            return View(item);
        }

        public ActionResult Criar(FeriasCriar FeriasNovo)
        {
            Ferias ferias = new Ferias();
            ConversorInsert.AplicarValores(FeriasNovo, ferias);

            //Pega o Funcionario pelo Email
            ferias.Funcionario = FuncionarioRepository.PesquisaPeloEmail(FeriasNovo.Funcionario);

            Repository.Salvar(ferias);
            Context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AprovarRejeitarFerias(Guid Id, RespostaSolicitacao resposta)
        {

            var ferias = Repository.PesquisarPeloId(Id);

            ferias.Resposta = resposta;
            if (resposta == RespostaSolicitacao.Aprovado)
            {

                switch (ferias.Tipo)
                {

                    case TipoSolicitacao.Ajuste:
                        if (resposta == RespostaSolicitacao.Aprovado)
                        {
                            return RedirectToAction("Index"); 
                        }
                        break;

                    case TipoSolicitacao.Inclusao:
                        if (resposta == RespostaSolicitacao.Aprovado)
                        {
                            return RedirectToAction("Index"); 
                            
                        }
                        break;

                    case TipoSolicitacao.Desconsideracao:
                        if (resposta == RespostaSolicitacao.Aprovado)
                        {
                            return RedirectToAction("Index");
                        }
                        break;

                }
            }

            Repository.Salvar(ferias);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}