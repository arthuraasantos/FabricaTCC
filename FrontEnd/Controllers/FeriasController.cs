using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using FrontEnd.Models.Conversores;
using Infraestrutura;
using Seedwork.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class FeriasController : BaseController<Ferias, FeriasCriar, FeriasAprovar>
    {
        // GET: Ferias
        private IFuncionarioRepository FuncionarioRepository { get; set; }

        private IFeriasRepository FeriasRepository { get; set; }

        public FeriasController(MyContext context, IFeriasRepository feriasRepository, IFuncionarioRepository funcionarioRepository)
            : base(context, feriasRepository, new FeriasToFeriasCriar(), new FeriasToFeriasAjustar())
        {
            FuncionarioRepository = funcionarioRepository;
            FeriasRepository = feriasRepository;
        }

        public ActionResult Solicitar()
        {
            FeriasCriar _ferias = new FeriasCriar();
            Funcionario _funcionario = new Funcionario();
            _funcionario = FuncionarioRepository.PesquisaPeloEmail(Sessao.FuncionarioLogado.Email.ToString());
            _ferias.Funcionario = _funcionario.Email;
            _ferias.Resposta = RespostaSolicitacao.Nenhuma;
            ViewBag.Mensagem = ViewBag.Mensagem;
            return View(_ferias);
        }

        public ActionResult Criar(string Funcionario, DateTime Inicio, DateTime Fim, string Justificativa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Ferias _ferias = new Ferias();
                    FeriasCriar _feriasCriar = new FeriasCriar();
                    _feriasCriar.Funcionario = Funcionario;
                    _feriasCriar.Inicio = Inicio;
                    _feriasCriar.Fim = Fim;
                    _feriasCriar.Funcionario = Funcionario;
                    _feriasCriar.Justificativa = Justificativa;
                    ConversorInsert.AplicarValores(_feriasCriar, _ferias);
                    _ferias.Funcionario = FuncionarioRepository.PesquisaPeloEmail(_feriasCriar.Funcionario);

                    Repository.Salvar(_ferias);
                    Context.SaveChanges();

                    TempData["Mensagem"] = "Suas férias foram solicitadas com sucesso!";

                    return RedirectToAction("Solicitar", "Ferias");

                }
                return RedirectToAction("Solicitar");

            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao solicitar férias!";
                return RedirectToAction("Solicitar");
            }

        }

        public override ActionResult Index()
        {
            var lista = Repository.
                            Listar().
                            Where(p => p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).
                            Where(p => p.Resposta == RespostaSolicitacao.Nenhuma).
                            OrderBy(o => new { o.Funcionario.Nome, o.Inicio, o.Fim }).

                            ToList();

            return View(lista);
        }

        public ActionResult AprovarRejeitarFerias(Guid Id, RespostaSolicitacao resposta)
        {

            var ferias = Repository.PesquisarPeloId(Id);
            ferias.Resposta = resposta;
            Context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}