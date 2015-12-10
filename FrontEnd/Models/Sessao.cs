using Dominio.Model;
using Seedwork.Const;


namespace FrontEnd.Models
{
    public static class Sessao
    {

        private static Funcionario _Funcionario;
        private static Empresa _Empresa;
        private static PerfilAcesso _Perfil;
        
        public static Funcionario FuncionarioLogado
        {
            get
            {
                _Funcionario = (Funcionario)System.Web.HttpContext.Current.Session["Funcionario"];
               
                return _Funcionario;
            }
        }
        public static Empresa EmpresaLogada
        {
            get
            {
                _Empresa = FuncionarioLogado.Empresa;
                return _Empresa;
            }
        }
        public static PerfilAcesso PerfilFuncionarioLogado
        { 
            get
            {
                switch (Sessao.FuncionarioLogado.PerfilDeAcesso.Descricao)
                {
                    case "Gerente/RH":
                        _Perfil = PerfilAcesso.Gerente;
                        break;
                    case "Administrador":
                        _Perfil = PerfilAcesso.Administrador;
                        break;
                    default:
                        _Perfil = PerfilAcesso.Funcionario;
                        break;
                }

                return _Perfil;
            }
        }
    }
}