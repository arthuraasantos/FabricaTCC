
namespace TCCPontoEletronico.AppService.Email
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
    }
}
