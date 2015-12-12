
using Dominio.Model;
using Dominio.Repository;
using Seedwork.Repository;

namespace Infraestrutura.Repositorios
{
    public class PerfilDeAcessoRepository : RepositoryBase<PerfilDeAcesso>, IPerfilDeAcessoRepository
    {
        private readonly MyContext Context;

        public PerfilDeAcessoRepository(MyContext context) : base(context)
        {
            Context = context;
        }
    }
}
