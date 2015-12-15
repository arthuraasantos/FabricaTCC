using Dominio.Model;
using Seedwork.Repository;
using System;

namespace Dominio.Repository
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        int GetCountOrganizations();
        Guid GetOrganizationId(string fantasyName);
    }
}
