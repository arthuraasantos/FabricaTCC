
using Dominio.Model;
using Dominio.Repository;
using Seedwork.Const;
using System;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;
using TCCPontoEletronico.AppService.Static;
using System.Collections.Generic;
using Infraestrutura.Importacao;

namespace TCCPontoEletronico.AppService.Entity
{
    public class FuncionarioService : IFuncionarioService, IImportacaoService
    {
        private readonly Funcionario _funcionario;
        private readonly IFuncionarioRepository funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionario = (Funcionario)System.Web.HttpContext.Current.Session["Funcionario"];
            this.funcionarioRepository = funcionarioRepository;
        }

        public string GetEmailEmployeeLogged() => _funcionario.Email;

        public string GetNameEmployeeLogged() => _funcionario.Nome;

        public string GetOrganizationNameLogged() => _funcionario.Empresa.NomeFantasia;

        public string GetAccessProfileDescription() => _funcionario.PerfilDeAcesso.Descricao;

        public Funcionario GetEmployeeLogged() => _funcionario;

        public PerfilAcesso GetAccessProfile()
        {
            switch (GetAccessProfileDescription())
            {
                case "Gerente/RH":
                    return PerfilAcesso.Gerente;
                case "Administrador":
                    return PerfilAcesso.Administrador;
                default:
                    return PerfilAcesso.Funcionario;
            }
        }

        public Guid GetOrganizationIdLogged() => _funcionario.Empresa.Id;

        public int GetCountEmployee() => funcionarioRepository.GetCount();

        public EmployeeDTO CreateEmployee(string employeeName, string employeeCpf, string employeeEmail, Guid organizationId, Guid officeHoursId)
        {
            try
            {
                Funcionario employee = new Funcionario();
                employee.Id = Guid.NewGuid();
                employee.Nome = employeeName;
                employee.Cpf = employeeCpf;
                employee.Email = employeeEmail;
                employee.Senha = "bemvindo";
                employee.PerfilDeAcesso = new PerfilDeAcesso();
                employee.PerfilDeAcesso.Id = AccessProfile.GetManagerId();
                employee.HorarioDeExpediente = new HorarioDeExpediente();
                employee.HorarioDeExpediente.Id = officeHoursId;
                employee.Empresa = new Empresa();
                employee.Empresa.Id = organizationId; 

                funcionarioRepository.Salvar(employee);
                return new EmployeeDTO
                {
                    Id = employee.Id,
                    Name = employee.Nome,
                    Email = employee.Email,
                    OrganizationFantasyName = employee.Empresa.NomeFantasia,
                    Password = employee.Senha
                };
            }
            catch (Exception)
            {
                //ToDo Implementar log de erro
                throw;
            }
        }

        public bool ImportarTextoPlanilha(List<string[]> textoPlanilha, string[] relacaoColunas, TipoImportacao tipoImportacao)
        {
            var importacao = new ImportacaoFuncionario(funcionarioRepository);
            importacao.ImportarTextoPlanilha(textoPlanilha,relacaoColunas,tipoImportacao);
            return true;
        }
    }
}
