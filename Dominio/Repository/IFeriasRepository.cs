using Dominio.Model;
using Seedwork.Repository;
using System;


namespace Dominio.Repository
{
    public interface IFeriasRepository : IRepository<Ferias>
    {
        int GetCountPendingVacation(Guid organizationId);
        int GetCountResponsePendingVacation(Guid organizationId);

    }
}
