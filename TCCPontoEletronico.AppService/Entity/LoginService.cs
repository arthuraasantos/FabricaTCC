
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
        private MyContext Context { get; }
        private IFuncionarioRepository FuncionarioRepository { get; }

        private readonly IOrganizationService OrganizationService;
        private readonly IEmployeeService EmployeeService;
        private readonly IOfficeHoursService OfficeHoursService;
        private readonly IEmailService EmailService;


        public LoginService(IOrganizationService organizationService, IEmployeeService employeeService, IOfficeHoursService officeHoursService,
                            IEmailService emailService)
        {
            Context = new MyContext();
            FuncionarioRepository = new FuncionarioRepository(Context);
            OrganizationService = organizationService;
            EmployeeService = employeeService;
            OfficeHoursService = officeHoursService;
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
                FuncionarioRepository.
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
            using (MyContext newContext = new MyContext())
            {
                using (var transactionContext = newContext.Database.BeginTransaction())
                    try
                    {
                        //Cria nova empresa 
                        var organization = OrganizationService.CreateOrganization(newRegister.OrganizationName);

                        // Criar novo horário de expediente
                        var officeHour = OfficeHoursService.CreateForLogin(organization.Id);

                        //Cria novo funcionário
                        var newEmployee = EmployeeService.CreateEmployee(newRegister.EmployeeName, newRegister.EmployeeCpf, newRegister.EmployeeEmail, organization.Id, officeHour.Id);

                        // Notifica usuário com dados de acesso
                        EmailService.SendMailNewUser(newRegister);

                        // ToDo Notificar CEOs
                        //EmailService.NotifyNewUserForCEOs();
                        newContext.SaveChanges();
                        transactionContext.Commit();
                    }

                    catch (Exception)
                    {
                        //ToDo Log de Erros
                        transactionContext.Rollback();
                        throw;
                    }
            }
        }
    }
}
