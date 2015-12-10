
using Dominio.Model;

namespace TCCPontoEletronico.AppService.Password
{
    public interface ILoginService
    {
        string IsValid(string email, string password);
        Funcionario GetSession();
        

    }
}
