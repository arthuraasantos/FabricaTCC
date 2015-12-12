
using Dominio.Model;
using Dominio.Repository;
using Seedwork.Const;
using System;
using TCCPontoEletronico.AppService.Interface;

namespace TCCPontoEletronico.AppService.Entity
{
    public class EmployeeService : IEmployeeService
    {
        private readonly Funcionario _Employee;
        private readonly IFuncionarioRepository FuncionarioRepository;

        public EmployeeService(IFuncionarioRepository funcionarioRepository)
        {
            _Employee = (Funcionario)System.Web.HttpContext.Current.Session["Funcionario"];
            FuncionarioRepository = funcionarioRepository;
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

        public int GetCountEmployee() => FuncionarioRepository.GetCount();
        
    }
}
