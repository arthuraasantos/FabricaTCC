
using System;
using Dominio.Model;
using Dominio.Repository;
using Infraestrutura;
using Infraestrutura.Repositorios;
using SeedWork.Tools;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Entity
{
    public class LoginService : ILoginService
    {
        private PontoContext Contexto;
        private IFuncionarioRepository _funcionarioRepositorio { get; }

        private readonly IEmpresaService _empresaServico;
        private readonly IFuncionarioService _funcionarioServico;
        private readonly IHorarioDeExpedienteService _horarioDeExpedienteServico;
        private readonly IEmailService EmailService;


        public LoginService(IEmpresaService organizationService, IFuncionarioService employeeService, IHorarioDeExpedienteService officeHoursService,
                            IEmailService emailService, PontoContext contexto)
        {
            Contexto = contexto;
            _funcionarioRepositorio = new FuncionarioRepository(Contexto);
            _empresaServico = organizationService;
            _funcionarioServico = employeeService;
            _horarioDeExpedienteServico = officeHoursService;
            EmailService = emailService;
        }
        public string IsValid(string email, string password)
        {
            string message = string.Empty;

            if (!EmailService.IsValid(email))
                message = "Preencha o e-mail";
            else if (!PasswordService.IsValid(password))
                message = "Preencha o campo senha";

            var funcionarioParaLogin = new Funcionario();
            funcionarioParaLogin =
                _funcionarioRepositorio.
                PesquisaParaLogin(
                    email,
                    Criptografia.Encrypt(password));

            if (funcionarioParaLogin == null)
            {
                message = "Usuário ou senha incorretos";
            }

            return message;
        }

        public Funcionario GetSession()
        {
            return (Funcionario)System.Web.HttpContext.Current.Session["Funcionario"];

        }

        public void NewLogin(NewRegisterDTO newRegister)
        {
            using (var transacao = Contexto.Database.BeginTransaction())
                try
                {
                    //Cria nova empresa 
                    var empresa = _empresaServico.CreateOrganization(newRegister.NomeFantasiaEmpresa, newRegister.CnpjEmpresa);
                    
                    // Criar novo horário de expediente
                    var horarioDeExpediente = _horarioDeExpedienteServico.CreateForLogin(empresa.Id);
                    
                    //Cria novo funcionário
                    var funcionario = _funcionarioServico.CriarFuncionario(newRegister.NomeFuncionario, newRegister.EmailFuncionario, empresa.Id, horarioDeExpediente.Id, newRegister.SenhaFuncionario);

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    //ToDo Log de Erros
                    transacao.Rollback();
                    throw;
                }
        }
    }
}
