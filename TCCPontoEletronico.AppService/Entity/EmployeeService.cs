
using Dominio.Model;
using Dominio.Repository;
using Seedwork.Const;
using System;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;
using TCCPontoEletronico.AppService.Static;

namespace TCCPontoEletronico.AppService.Entity
{
    public class EmployeeService : IEmployeeService
    {
        private readonly Funcionario _Employee;
        private readonly IFuncionarioRepository EmployeeRepository;

        public EmployeeService(IFuncionarioRepository employeeRepository)
        {
            _Employee = (Funcionario)System.Web.HttpContext.Current.Session["Funcionario"];
            EmployeeRepository = employeeRepository;
        }

        public string GetEmailEmployeeLogged() => _Employee.Email;

        public string GetNameEmployeeLogged() => _Employee.Nome;

        public string GetOrganizationNameLogged() => _Employee.Empresa.NomeFantasia;

        public string GetAccessProfileDescription() => _Employee.PerfilDeAcesso.Descricao;

        public Funcionario GetEmployeeLogged() => _Employee;

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

        public Guid GetOrganizationIdLogged() => _Employee.Empresa.Id;

        public int GetCountEmployee() => EmployeeRepository.GetCount();

        public EmployeeDTO CreateEmployee(string employeeName, string employeeCpf, string employeeEmail, Guid organizationId, Guid officeHoursId)
        {
            try
            {
                Funcionario employee = new Funcionario();
                employee.Id = Guid.NewGuid();
                employee.Nome = employeeName;
                employee.Cpf = employeeCpf;
                employee.Email = employeeEmail;
                employee.PerfilDeAcesso = new PerfilDeAcesso();
                employee.PerfilDeAcesso.Id = AccessProfile.GetManagerId();
                employee.HorarioDeExpediente = new HorarioDeExpediente();
                employee.HorarioDeExpediente.Id = officeHoursId;
                employee.Empresa = new Empresa();
                employee.Empresa.Id = organizationId; 

                EmployeeRepository.Salvar(employee);
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
    }
}
