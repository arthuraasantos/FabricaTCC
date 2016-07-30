using Dominio;
using Dominio.Model;
using Dominio.Repository;
using Infraestrutura.UnitOfWork;
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
        public readonly PontoContext Context;
        public FuncionarioRepository(PontoContext context) : base(context)
        {
            Context = context;
        }

        public Funcionario PesquisaParaLogin(string email, string senha)
        {
            try
            {
                return Context.Set<Funcionario>().Where(f => f.Email.Equals(email) && f.Senha.Equals(senha)).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Funcionario PesquisaPeloEmail(string email)
        {
            try
            {
                return Context.Set<Funcionario>().Where(f => f.Email.Equals(email)).SingleOrDefault();
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

        public int GetCount() => 
            Context.Set<Funcionario>().Count();
    }
}
