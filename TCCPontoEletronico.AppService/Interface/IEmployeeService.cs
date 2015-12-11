
using Dominio.Model;
using Seedwork.Const;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IEmployeeService
    {
        string GetEmailEmployeeLogged();
        string GetNameEmployeeLogged();
        string GetOrganizationNameLogged();
        PerfilAcesso GetAccessProfile();
        string GetAccessProfileDescription();
        Funcionario GetEmployeeLogged();
        
    }
}
