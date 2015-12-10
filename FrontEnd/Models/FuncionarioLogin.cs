
namespace FrontEnd.Models
{
    public class FuncionarioLogin
    {

        public FuncionarioLogin(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public string Email { get; set; }
        public string Password { get; set; }


        
    }

}