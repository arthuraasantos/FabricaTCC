
using Seedwork.Repository;
using Dominio.Repository;
using Dominio.Model;

namespace Infraestrutura.Repositorios
{
    public class FeriasRepository : RepositoryBase<Ferias>, IFeriasRepository
    {
        private readonly MyContext Context;
        public FeriasRepository(MyContext context): base(context)
        {
            Context = context;
        }
    }
}
