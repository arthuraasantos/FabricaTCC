using Dominio;
using Dominio.Model;
using Dominio.Repository;
using Seedwork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorios
{
    public class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
    {
        public MyContext Contexto { get; set; }
        public FuncionarioRepository(MyContext context) : base(context)
        {
            Contexto = context;
        }

        public Funcionario PesquisaParaLogin(string email, string senha)
        {
            try
            {
                return Contexto.Set<Funcionario>().Where(f => f.Email.Equals(email) && f.Senha.Equals(senha)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public Funcionario PesquisaPeloEmail(string email)
        {
            try
            {
                return Contexto.Set<Funcionario>().Where(f => f.Email.Equals(email)).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IQueryable<Funcionario> ListarComPerfil( PerfilDeAcesso Perfil )
        {
            var Lista = base.Listar();
            if (Perfil.Descricao != "Administrador")
            {
                Lista = Lista.Where(p => p.PerfilDeAcesso.Descricao != "Administrador");
            }
            return Lista;
        }

    }
}
