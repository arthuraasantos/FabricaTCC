using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Seedwork.Const;
using SeedWork.Tools;
using TCCPontoEletronico.AppService.Interface;

namespace FrontEnd.Models
{
    //[Authorize]
    public class PontoController : Controller
    {
        PontoContext Context;
        private IPontoRepository PontoRepository { get; set; }
        private IPontoEletronicoService PontoEletronicoService { get; set; }
        private IFuncionarioRepository FuncionarioRepository { get; set; }


        public PontoController(PontoContext context, IPontoRepository pontoRepository, IPontoEletronicoService pontoEletronicoService)
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
            
            Funcionario _Funcionario = new Funcionario();
            _Funcionario = FuncionarioRepository.PesquisaPeloEmail(_Email);

            // Pega a data, ou default data atual
            DateTime _Data = DateTime.Now;
            if (Data != null) { _Data = DateTime.Parse(Data.ToString()); }



            var ListaFuncionarios = FuncionarioRepository.ListarComPerfil(Sessao.FuncionarioLogado.PerfilDeAcesso).ToList();

            switch (Sessao.PerfilFuncionarioLogado)
            {
                case PerfilAcesso.Funcionario:
                    ListaFuncionarios = ListaFuncionarios.Where(p => p.Id == Sessao.FuncionarioLogado.Id).ToList();
                    break;
                case PerfilAcesso.Gerente: 
                    ListaFuncionarios = ListaFuncionarios.Where(p => p.Empresa.Id == Sessao.EmpresaLogada.Id).ToList();
                    break;
            }


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
                DicionarioHoras.Add(_PrimeiraData.AddDays(i), PontoEletronicoService.QuantidadeDeHorasTrabalhadasPorFuncionarioPorDia(_Funcionario, _PrimeiraData.AddDays(i)));
            }


            ViewBag.MaiorBatidas = ((int)((Maior + 1) / 2)) * 2;
            ViewBag.DicionarioHoras = DicionarioHoras;
            ViewBag.ListaFuncionarios = ListaFuncionarios.Select(p => new SelectListItem() { Text = p.Email, Value = p.Email });

            return View(Dicionario);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RelatorioMarcacoes(DateTime? Data)
        {
            DateTime _Data = DateTime.MinValue;
            if (Data != null)
            {
                _Data = Data.GetValueOrDefault();
                ViewBag.Data = Data.GetValueOrDefault().ToString();
            }
            else
            {
                ViewBag.Data = "";
            }

            var temp_listaFull = PontoRepository.
                             Listar().
                             ToList().
                             Where(p => p.DataMarcacao.HasValue).
                             ToList();

            var _listaFull = temp_listaFull.Where(p => p.DataMarcacao.GetValueOrDefault(DateTime.MaxValue).Date == _Data.Date).ToList();


            Dictionary<Funcionario, List<Ponto>> Dicionario = new Dictionary<Funcionario, List<Ponto>>();
            foreach (var a in _listaFull)
            {
                if (Sessao.PerfilFuncionarioLogado == PerfilAcesso.Administrador || a.Funcionario.Empresa.Id == Sessao.EmpresaLogada.Id)
                {
                    if (!Dicionario.ContainsKey(a.Funcionario))
                    {
                        var _lista = _listaFull.Where(p => p.Funcionario.Id == a.Funcionario.Id).ToList();
                        Dicionario.Add(a.Funcionario, _lista);
                    }
                }
            }
            return View(Dicionario);
        }
    }
}