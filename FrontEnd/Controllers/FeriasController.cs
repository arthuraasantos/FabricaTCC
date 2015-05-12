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

        public FeriasController(MyContext context, IFeriasRepository feriasRepository, IFuncionarioRepository funcionarioRepository)
            : base(context, feriasRepository, new FeriasToFeriasCriar(), new FeriasToFeriasAjustar())
        {
            FuncionarioRepository = funcionarioRepository;
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
                Ferias _ferias = new Ferias();
                FeriasCriar _feriasCriar = new FeriasCriar();
                _feriasCriar.Funcionario = Funcionario;
                _feriasCriar.Inicio = Inicio;
//                _feriasCriar.Inicio = DateTime.Parse(Inicio);
                _feriasCriar.Fim = Fim;
                //_feriasCriar.Fim = DateTime.Parse(Fim);
                _feriasCriar.Funcionario = Funcionario;
                ConversorInsert.AplicarValores(_feriasCriar, _ferias);
                _ferias.Funcionario = FuncionarioRepository.PesquisaPeloEmail(_feriasCriar.Funcionario);

                Repository.Salvar(_ferias);
                Context.SaveChanges();
                return RedirectToAction("Index", "Home", TempData["Mensagem"] = "Férias solicitadas com sucesso!");

            }
            catch (Exception e)
            {
                ViewBag.Mensagem = "Erro ao solicitar férias. Erro: "+ e.Message;
                return RedirectToAction("Index", "Ferias", TempData["Mensagem"] = "Erro ao solicitar férias!");

            }
        }
    }
}