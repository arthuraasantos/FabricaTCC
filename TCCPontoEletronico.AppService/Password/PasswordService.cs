

namespace TCCPontoEletronico.AppService.Password
{
    public static class PasswordService
    {
        public static bool IsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Contains(">="))
                return false;

            if (password.Contains("<="))
                return false;

            return true;
        }


    }
}
