using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using FrontEnd.Models.Conversores;
using Infraestrutura;
using Seedwork.Const;
using Seedwork.Entity;
using SeedWork.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace FrontEnd.Models
{

    public class FuncionarioController : BaseController<Funcionario, FuncionarioNovo, FuncionarioEditar>
    {
        public IPerfilDeAcessoRepository PerfildeacessoRepository;
        public IEmpresaRepository EmpresaRepository;
        public IFuncionarioRepository FuncionarioRepository;
        public IHorarioDeExpedienteRepository HorarioDeExpedienteRepository;

        private IEnumerable<SelectListItem> ListaPerfis;
        private IEnumerable<SelectListItem> ListaEmpresas;
        private IEnumerable<SelectListItem> ListaHorariosDeExpediente;


        public FuncionarioController(PontoContext context, IFuncionarioRepository funcionarioRepository, IPerfilDeAcessoRepository perfildeacessoRepository, IEmpresaRepository empresaRepository, IHorarioDeExpedienteRepository horarioDeExpedienteRepository)
            : base(context, funcionarioRepository, new FuncionarioToFuncionarioNovo(perfildeacessoRepository, empresaRepository, horarioDeExpedienteRepository), new FuncionarioToFuncionarioEditar(empresaRepository, perfildeacessoRepository, horarioDeExpedienteRepository))
        {
            PerfildeacessoRepository = perfildeacessoRepository;
            EmpresaRepository = empresaRepository;
            FuncionarioRepository = funcionarioRepository;
            HorarioDeExpedienteRepository = horarioDeExpedienteRepository;

            // Pega a lista de Perfis de Acesso por permissao
            switch (Sessao.PerfilFuncionarioLogado)
            {

                case PerfilAcesso.Administrador: // Traz todos os Perfis
                    ListaPerfis = PerfildeacessoRepository
                                    .Listar()
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                    break;
                case PerfilAcesso.Gerente: // Traz os Perfis Gerente e Funcionario
                    ListaPerfis = PerfildeacessoRepository
                                    .Listar()
                                    .Where(p => p.Descricao != "Administrador")
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                    break;
                case PerfilAcesso.Funcionario: // Traz somente o Perfil Funcionario
                    ListaPerfis = PerfildeacessoRepository
                                    .Listar()
                                    .Where(p => p.Descricao == "FuncionarioComum")
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                    break;
            }


            switch (Sessao.PerfilFuncionarioLogado)
            {

                case PerfilAcesso.Administrador: // Traz todos as Empresas
                    ListaEmpresas = EmpresaRepository
                                    .Listar()
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.NomeFantasia, Value = p.Id.ToString() });
                    break;
                default:
                    ListaEmpresas = EmpresaRepository
                                    .Listar()
                                    .Where(e => e.Id == Sessao.EmpresaLogada.Id)
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.NomeFantasia, Value = p.Id.ToString() });
                    break;
            }

            switch (Sessao.PerfilFuncionarioLogado)
            {

                case PerfilAcesso.Administrador: // Traz todos os Horários de Expediente
                    ListaHorariosDeExpediente = HorarioDeExpedienteRepository
                                    .Listar()
                                    .Where(p => p.Id != null)
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                    break;
                default:
                    ListaHorariosDeExpediente = HorarioDeExpedienteRepository
                                    .Listar()
                                    .Where(e => e.Empresa.Id == Sessao.EmpresaLogada.Id)
                                    .ToList()
                                    .Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
                    break;
            }

        }


        public override ActionResult Visualizar(Guid Id)
        {
            try
            {
                var uf = new UF();

                ViewBag.ListagemdeEmpresas = ListaEmpresas;
                ViewBag.ListagemdePerfis = ListaPerfis;
                ViewBag.ListagemdeHorariosDeExpediente = ListaHorariosDeExpediente;
                ViewBag.ListagemdeUF = uf.Listar().ToList().Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Valor });
                return base.Visualizar(Id);

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro ao editar funcionário: " + e.Message;
                return RedirectToAction("Index");
            }

        }
        public override ActionResult Novo()
        {
            FuncionarioNovo novo = new FuncionarioNovo();
            Empresa emp = (Empresa)TempData["Empresa"];

            if (TempData["novoFuncionario"] != null)
            {
                novo = (FuncionarioNovo)TempData["novoFuncionario"];
                novo.IdEmpresa = Sessao.EmpresaLogada.Id;
                novo.Empresas = ListaEmpresas;
                novo.HorarioDeExpedientes = ListaHorariosDeExpediente;
                novo.PerfisDeAcesso = ListaPerfis;
            }
            else
            {
                novo = new FuncionarioNovo()
                {
                    IdEmpresa = Sessao.EmpresaLogada.Id,
                    Empresas = ListaEmpresas,
                    PerfisDeAcesso = ListaPerfis,
                    HorarioDeExpedientes = ListaHorariosDeExpediente
                };
                // Quando cadastra uma empresa, vem direto para o cadastro do funcionario com a empresa cadastrada
                if (emp != null)
                {
                    TempData["Mensagem"] = "Empresa cadastrada com sucesso!";
                    novo.IdEmpresa = emp.Id;
                }
            }

            return View("Novo", novo);
        }


        private object PerfilDeAcessoPadrao()
        {
            throw new NotImplementedException();
        }
        public override ActionResult Incluir(FuncionarioNovo novo)
        {

            try
            {

                if (novo.Cpf == null || novo.Cpf == string.Empty)
                {
                    TempData["MensagemAtencao"] = "Campo CPF obrigatório!";
                    TempData["novoFuncionario"] = novo;
                    return RedirectToAction("Novo", novo);
                }
                else if (!SeedWork.Tools.Validacao.IsCPFValid(novo.Cpf))
                {
                    TempData["MensagemAtencao"] = "O CPF digitado não é válido!";
                    TempData["novoFuncionario"] = novo;
                    return RedirectToAction("Novo", novo);
                }
                else if (novo.Email == string.Empty || novo.Email == null)
                {
                    TempData["MensagemAtencao"] = "Campo Email obrigatório!";
                    TempData["novoFuncionario"] = novo;
                    return RedirectToAction("Novo", novo);
                }
                else if (novo.Senha == string.Empty || novo.Senha == null)
                {
                    TempData["MensagemAtencao"] = "Campo Senha obrigatório!";
                    TempData["novoFuncionario"] = novo;
                    return RedirectToAction("Novo", novo);
                }
                else if (novo.Senha != novo.SenhaConfirmacao)
                {
                    TempData["MensagemAtencao"] = "Campo senha ou confirmação de senha inválidos! As senhas devem ser iguais";
                    TempData["novoFuncionario"] = novo;
                    return RedirectToAction("Novo", novo);
                }
                else if (novo.Nome == string.Empty || novo.Nome == null)
                {
                    TempData["MensagemAtencao"] = "Campo Nome obrigatório! ";
                    TempData["novoFuncionario"] = novo;
                    return RedirectToAction("Novo", novo);
                }

                var entity = ConversorInsert.Converter(novo);
                entity.Id = Guid.NewGuid();

                Repository.Salvar(entity);
                Context.SaveChanges();

                TempData["Mensagem"] = "Funcionário cadastrado com sucesso!";
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro ao cadastrar funcionário! " + e.Message;
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public JsonResult AtualizaDadosEndereco(string cep)
        {
            Tools t = new Tools();
            return this.Json(t.BuscaCep(cep), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AtualizaListaHorarios(string Empresa)
        {
            Guid empresaGuid = Guid.Parse(Empresa);
            var listaHorarios = HorarioDeExpedienteRepository.Listar().Where(a => a.Empresa.Id == empresaGuid).AsQueryable();
            var lista = new List<SelectListItem>();
            foreach (var x in listaHorarios)
            {
                var item = new SelectListItem { Value = x.Id.ToString(), Text = x.Descricao };
                lista.Add(item);
            }
            return this.Json(lista, JsonRequestBehavior.AllowGet);
        }
        public override ActionResult Index()
        {
            List<Funcionario> lista = new List<Funcionario>();

            switch (Sessao.PerfilFuncionarioLogado)
            {
                case PerfilAcesso.Gerente: // Se for administrador do sistema, mostrar todas os funcionários da mesma empresa
                    lista = FuncionarioRepository.ListarComPerfil(Sessao.FuncionarioLogado.PerfilDeAcesso).Where(f => f.Empresa.Id == Sessao.EmpresaLogada.Id).ToList();
                    break;
                case PerfilAcesso.Funcionario: // Se for administrador do sistema, mostrar somente o funcionário logado
                    lista = FuncionarioRepository.ListarComPerfil(Sessao.FuncionarioLogado.PerfilDeAcesso).Where(f => f.Id == Sessao.FuncionarioLogado.Id).ToList();
                    break;
                case PerfilAcesso.Administrador: // Se for administrador do sistema, mostrar todas os funcionários
                    lista = FuncionarioRepository.ListarComPerfil(Sessao.FuncionarioLogado.PerfilDeAcesso).ToList();
                    break;
            }

            return View("Index", lista);
        }
        [HttpGet]
        public JsonResult BloquearFuncionario(string Id)
        {
            try
            {
                var idfuncionario = Id.Replace("{", "").Replace("}", "").Replace("id =", "");
                var funcionario =
                (Funcionario)FuncionarioRepository.PesquisarPeloId(Guid.Parse(idfuncionario));
                if (funcionario != null)
                {
                    funcionario.Bloqueado = "Y";
                    FuncionarioRepository.Salvar(funcionario);
                    Context.SaveChanges();
                }
                return this.Json(funcionario.Nome, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public JsonResult DesbloquearFuncionario(string Id)
        {
            try
            {
                var idfuncionario = Id.Replace("{", "").Replace("}", "").Replace("id =", "");
                var funcionario =
                (Funcionario)FuncionarioRepository.PesquisarPeloId(Guid.Parse(idfuncionario));
                if (funcionario != null)
                {
                    funcionario.Bloqueado = "N";
                    FuncionarioRepository.Salvar(funcionario);
                    Context.SaveChanges();
                }
                return this.Json(funcionario.Nome, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult AlterarSenha(Guid id)
        {

            var func = Repository.PesquisarPeloId(id);
            var modelAlterarSenha = new FuncionarioAlterarSenha()
            {
                Id = func.Id,
                Nome = func.Nome
            };

            return View(modelAlterarSenha);
        }
        public ActionResult GravaAlteracaoSenha(FuncionarioAlterarSenha funcSenha)
        {
            try
            {
                var entity = Repository.PesquisarPeloId(funcSenha.Id);

                if (entity.Senha == Criptografia.Encrypt(funcSenha.SenhaAtual))
                {
                    if (Criptografia.Encrypt(funcSenha.SenhaNova) == Criptografia.Encrypt(funcSenha.SenhaNovaConfirmacao))
                    {
                        entity.Senha = Criptografia.Encrypt(funcSenha.SenhaNova);

                        Repository.Salvar(entity);
                        Context.SaveChanges();

                        // Se funcionario editado for o usuário logado, atualiza a Sessao do usuário logado...
                        if (entity.Id == Sessao.FuncionarioLogado.Id)
                        {
                            Session.Remove("Funcionario");
                            Session.Add("Funcionario", entity);
                        }


                        TempData["Mensagem"] = "Senha alterada com sucesso!";
                    }
                    else
                    {
                        TempData["MensagemErro"] = "A confirmação de senha não confere com a nova senha! Tente novamente!";
                    }
                }
                else
                {
                    TempData["MensagemErro"] = "Senha informada não confere com a antiga! Tente novamente!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro ao alterar a senha do funcionário!" + e.Message;
                return RedirectToAction("Index");
            }
        }
        public override ActionResult Editar(FuncionarioEditar editar)
        {
            try
            {
                if (editar.Cpf == null || editar.Cpf == string.Empty)
                {
                    TempData["MensagemAtencao"] = "Campo CPF obrigatório!";
                    TempData["novoFuncionario"] = editar;
                    return RedirectToAction("Novo", editar);
                }
                else if (!SeedWork.Tools.Validacao.IsCPFValid(editar.Cpf))
                {
                    TempData["MensagemAtencao"] = "O CPF digitado não é válido!";
                    return RedirectToAction("Novo");
                }
                else if (editar.Email == string.Empty || editar.Email == null)
                {
                    TempData["MensagemAtencao"] = "Campo Email obrigatório!";
                    TempData["novoFuncionario"] = editar;
                    return RedirectToAction("Novo", editar);
                }
                else if (editar.Nome == string.Empty || editar.Nome == null)
                {
                    TempData["MensagemAtencao"] = "Campo Nome obrigatório! ";
                    TempData["novoFuncionario"] = editar;
                    return RedirectToAction("Novo", editar);
                }

                var entity = Repository.PesquisarPeloId(editar.Id);
                ConversorEdit.AplicarValores(editar, entity);
                Repository.Salvar(entity);
                Context.SaveChanges();

                // Se funcionario editado for o usuário logado, atualiza a Sessao do usuário logado...
                if (entity.Id == Sessao.FuncionarioLogado.Id)
                {
                    Session.Remove("Funcionario");
                    Session.Add("Funcionario", entity);
                }

                TempData["Mensagem"] = "Funcionário alterado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro ao alterar o funcionário! " + e.Message;
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public bool ValidaCPF(string aCPF)
        {
            return Validacao.IsCPFValid(aCPF);
        }

    }
}