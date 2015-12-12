using Dominio.Model;
using Seedwork.Repository;
using System;

namespace Dominio.Repository
{
    public interface IFolgaRepository: IRepository<Folga>
    {
        int GetCountPendingClearance(Guid organizationId);
        int GetCountResponsePendingClearance(Guid organizationId);
    }
}
