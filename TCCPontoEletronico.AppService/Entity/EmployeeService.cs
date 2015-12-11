
using Dominio.Model;
using Seedwork.Const;
using System;
using TCCPontoEletronico.AppService.Interface;

namespace TCCPontoEletronico.AppService.Entity
{
    public class EmployeeService : IEmployeeService
    {
        private Funcionario _Employee { get; set; }

        public EmployeeService()
        {
            _Employee = (Funcionario)System.Web.HttpContext.Current.Session["Funcionario"];
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
    }
}
