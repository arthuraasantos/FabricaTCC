
using Dominio.Model;
using Seedwork.Const;
using System;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IEmployeeService
    {
        string GetEmailEmployeeLogged();
        string GetNameEmployeeLogged();
        string GetOrganizationNameLogged();
        Guid GetOrganizationIdLogged();
        PerfilAcesso GetAccessProfile();
        string GetAccessProfileDescription();
        Funcionario GetEmployeeLogged();
        int GetCountEmployee();
        
    }
}
