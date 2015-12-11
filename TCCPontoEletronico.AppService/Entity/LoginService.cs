
using Dominio.Model;
using Dominio.Repository;
using Infraestrutura;
using Infraestrutura.Repositorios;
using SeedWork.Tools;
using TCCPontoEletronico.AppService.Interface;

namespace TCCPontoEletronico.AppService.Entity
{
    public class LoginService : ILoginService
    {
        private MyContext Context { get; }
        private IFuncionarioRepository FuncionarioRepository { get; }

        public LoginService()
        {
            Context = new MyContext();
            FuncionarioRepository = new FuncionarioRepository(Context);
        }
        public string IsValid(string email, string password)
        {
            string message = string.Empty;

            if (!EmailService.IsValid(email))
                message = "Preencha o e-mail";
            else if (!PasswordService.IsValid(password))
                message = "Preencha o campo senha";

            var funcionarioParaLogin = new Funcionario();
            funcionarioParaLogin =
                FuncionarioRepository.
                PesquisaParaLogin(
                    email,
                    Criptografia.Encrypt(password));

            if (funcionarioParaLogin == null)
            {
                message = "Usuário ou senha incorretos";
            }

            return message;
        }

        public Funcionario GetSession()
        {
            return (Funcionario)System.Web.HttpContext.Current.Session["Funcionario"];

        }
    }
}
