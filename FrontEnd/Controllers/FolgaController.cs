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
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class FolgaController : BaseController<Folga, FolgaCriar, FolgaAprovar>
    {
        private IFuncionarioRepository FuncionarioRepository { get; set; }

        private IFolgaRepository FolgaRepository { get; set; }

        public FolgaController(MyContext context, IFuncionarioRepository funcionarioRepository, IFolgaRepository folgaRepository)
            : base(context, folgaRepository, new FolgaToFolgaCriar(), new FolgaToFolgaAprovar())
        {
            FuncionarioRepository = funcionarioRepository;
            FolgaRepository = folgaRepository;
        }
        // GET: Folga
        public override ActionResult Index()
        {
            var lista = Repository.
                            Listar().
                            Where(p => p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).
                            Where(p => p.Resposta == RespostaSolicitacao.Nenhuma).
                            OrderBy(o => new { o.Funcionario.Nome, o.Data }).
                            ToList();

            return View(lista);
        }

        public ActionResult Solicitar()
        {
            FolgaCriar _folga = new FolgaCriar();
            Funcionario _funcionario = new Funcionario();
            _funcionario = FuncionarioRepository.PesquisaPeloEmail(Sessao.FuncionarioLogado.Email.ToString());
            _folga.Funcionario = _funcionario.Email;
            _folga.Resposta = RespostaSolicitacao.Nenhuma;
            return View(_folga);
        }

        public ActionResult AprovarRejeitarFolga(Guid Id, RespostaSolicitacao resposta)
        {
            try
            {
                var folga = FolgaRepository.PesquisarPeloId(Id);
                folga.Resposta = resposta;
                Context.SaveChanges();

                TempData["Mensagem"] = "Folga Aprovada/Rejeitada!";
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao Aprovar/Rejeitar folga!";
                throw;
            }
            
            return RedirectToAction("Index");
        }


        public ActionResult Criar(string Funcionario, DateTime Data, string Justificativa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Folga _folga = new Folga();
                    FolgaCriar _folgaCriar = new FolgaCriar();
                    _folgaCriar.Funcionario = Funcionario;
                    _folgaCriar.Data = Data;
                    _folgaCriar.Justificativa = Justificativa;
                    ConversorInsert.AplicarValores(_folgaCriar, _folga);
                    _folga.Funcionario = FuncionarioRepository.PesquisaPeloEmail(_folgaCriar.Funcionario);

                    Repository.Salvar(_folga);
                    Context.SaveChanges();

                    TempData["Mensagem"] = "Sua folga foi solicitada com sucesso!";

                    return RedirectToAction("Solicitar", "Folga");

                }
                return RedirectToAction("Solicitar");

            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao solicitar folga!";
                return RedirectToAction("Solicitar");
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
            var _ListaCompleta = FolgaRepository.
                            Listar().
                            Where(p => p.Data.Month == _Data.Month).
                            Where(p => p.Data.Year == _Data.Year).
                            Where(p => p.Funcionario.Email == _Email).
                            OrderBy(o => new { o.Funcionario.Nome, o.Data }).
                            ToList();

            return View("Index", _ListaCompleta);
        }
    }
}