using Dominio.Model;
using Dominio.Repository;
using Seedwork.Repository;
using System.Linq;

namespace Infraestrutura.Repositorios
{
    public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
    {
        private readonly MyContext Context;
        public EmpresaRepository(MyContext context) : base(context)
        {
            Context = context;
        }

        public int GetCountOrganizations() => Context.Set<Empresa>().Count();
    }
}
