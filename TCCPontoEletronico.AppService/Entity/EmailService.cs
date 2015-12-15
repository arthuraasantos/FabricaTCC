
using System;

namespace TCCPontoEletronico.AppService.Entity
{
    public static class EmailService
    {
        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (email.Contains(">="))
                return false;

            if (email.Contains("<="))
                return false;

            return true;
        }

        internal static bool SendEmail()
        {
            //TODO implementar método de envio de e-mails
            return false;
        }

        public static void NotifyNewUserForCEOs()
        {
            //TODO implementar
            // Enviar e-mail para ARTHUR, MARLON e CHARLES
            throw new NotImplementedException();
        }
    }
}
