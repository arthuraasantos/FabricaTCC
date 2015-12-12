using Dominio.Model;
using Seedwork.Repository;
using System;

namespace Dominio.Repository
{
    public interface ISolicitacaoRepository : IRepository<Solicitacao>
    {
        int GetCountPendingHours(Guid organizationId);
        int GetCountResponsePendingHours(Guid organizationId);

    }
}
