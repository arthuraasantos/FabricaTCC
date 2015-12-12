using Dominio.Model;
using Seedwork.Repository;

namespace Dominio.Repository
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        int GetCountOrganizations();
        
    }
}
