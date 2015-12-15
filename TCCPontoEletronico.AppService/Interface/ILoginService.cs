
using Dominio.Model;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface ILoginService
    {
        string IsValid(string email, string password);
        Funcionario GetSession();
        void NewLogin(NewRegisterDTO newRegister);

    }
}
