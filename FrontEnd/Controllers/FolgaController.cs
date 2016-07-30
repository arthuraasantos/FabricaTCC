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

namespace FrontEnd.Models
{
    public class FolgaController : BaseController<Folga, FolgaCriar, FolgaAprovar>
    {
        private IFuncionarioRepository FuncionarioRepository { get; set; }
        private IFolgaRepository FolgaRepository { get; set; }
        private IFeriasRepository FeriasRepository { get; set; }


        public FolgaController(PontoContext context, IFuncionarioRepository funcionarioRepository, IFolgaRepository folgaRepository, IFeriasRepository feriasRepository)
            : base(context, folgaRepository, new FolgaToFolgaCriar(), new FolgaToFolgaAprovar())
        {
            FuncionarioRepository = funcionarioRepository;
            FolgaRepository = folgaRepository;
            FeriasRepository = feriasRepository;
        }

        
        public override ActionResult Index()
        {
            if (Sessao.PerfilFuncionarioLogado == PerfilAcesso.Gerente)
            {
                var lista = Repository.
                            Listar().
                            Where(p => p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).
                            Where(p => p.Resposta == RespostaSolicitacao.Nenhuma).
                            OrderBy(o => new { o.Funcionario.Nome, o.Data }).
                            ToList();
                return View(lista);
            }
            else
            {
                if (Sessao.PerfilFuncionarioLogado == PerfilAcesso.Funcionario)
                {
                    var lista = Repository.
                                Listar().
                                Where(p => p.Funcionario.Id == Sessao.FuncionarioLogado.Id).
                                Where(p => p.Resposta == RespostaSolicitacao.Nenhuma).
                                OrderBy(o => new { o.Funcionario.Nome, o.Data }).
                                ToList();
                    return View(lista);
                }
                else
                {
                    var lista = Repository.
                                Listar().
                                Where(p => p.Resposta == RespostaSolicitacao.Nenhuma).
                                OrderBy(o => new { o.Funcionario.Nome, o.Data }).
                                ToList();
                    return View(lista);
                }
            }
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
                    // Valida se já existe folga aprovada para essa data...
                    bool ExistsFolga = FolgaRepository.Listar().Where(p => p.Data == Data.Date && p.Resposta == RespostaSolicitacao.Aprovado && p.Funcionario.Email == Funcionario).Count() > 0;
                    if (ExistsFolga)
                    {
                        TempData["MensagemAtencao"] = "Já existe uma folga aprovada para este dia! Verifique!";
                        return RedirectToAction("Solicitar", "Folga");
                    }

                    // Valida se a folga está dentro de um período de férias
                    bool ExistsFerias = FeriasRepository.Listar().Where(p => p.Inicio <= Data.Date && p.Fim >= Data.Date && p.Funcionario.Email == Funcionario && p.Resposta == RespostaSolicitacao.Aprovado).Count() > 0;
                    if (ExistsFerias)
                    {
                        TempData["MensagemAtencao"] = "Este dia está dentro de um período de férias! Verifique!";
                        return RedirectToAction("Solicitar", "Folga");
                    }

                    // Cria a folga
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
        public ActionResult Respostas()
        {
            if (Sessao.PerfilFuncionarioLogado == PerfilAcesso.Gerente)
            {
                var _lista = Repository.
                            Listar().
                            ToList().
                            Where(p => p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).
                            Where(p => p.Resposta != RespostaSolicitacao.Nenhuma).
                            OrderByDescending(p => p.Data).
                            ToList();
                return View(_lista);
            }
            else
            {
                if (Sessao.PerfilFuncionarioLogado == PerfilAcesso.Funcionario)
                {
                    var _lista = Repository.
                                Listar().
                                ToList().
                                Where(p => p.Funcionario.Id == Sessao.FuncionarioLogado.Id).
                                Where(p => p.Resposta != RespostaSolicitacao.Nenhuma).
                                OrderByDescending(p => p.Data).
                                ToList();
                    return View(_lista);
                }
                else
                {
                    var _lista = Repository.
                                Listar().
                                ToList().
                                Where(p => p.Resposta != RespostaSolicitacao.Nenhuma).
                                OrderByDescending(p => p.Data).
                                ToList();
                    return View(_lista);
                }
            }

        }
        public ActionResult RelatorioFolgasPorMesAno(string MesAno)
        {

            DateTime _MesAno = DateTime.MinValue;
            if (MesAno != null)
            {
                _MesAno = DateTime.Parse(MesAno + "/01");
                ViewBag.MesAno = MesAno;
            }
            else
            {
                ViewBag.MesAno = "";
            }

            var _lista = Repository.
                        Listar().
                        Where(p => p.Data.Month == _MesAno.Month || p.Data.Year == _MesAno.Year).
                        Where(p => p.Resposta == RespostaSolicitacao.Aprovado).
                        OrderBy(o => new { o.Data }).
                        ToList();

            if (Sessao.PerfilFuncionarioLogado == PerfilAcesso.Gerente)
            {
                return View(_lista.Where(p => p.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id).ToList());
            }
            else
            {
                return View(_lista);
            }
        }
    }
}