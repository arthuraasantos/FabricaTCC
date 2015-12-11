
using Dominio.Model;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface ILoginService
    {
        string IsValid(string email, string password);
        Funcionario GetSession();
        

    }
}
