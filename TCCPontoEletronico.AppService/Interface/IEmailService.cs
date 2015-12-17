
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IEmailService
    {
        bool IsValid(string email);
        void SendDefaultMail(string receiver, string subject, string bodyMessage);
        void SendMailNewUser(NewRegisterDTO user);
        void NotifyNewUserForCEOs(string employeeId);
        
    }
}
