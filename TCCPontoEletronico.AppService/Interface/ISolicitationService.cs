using System;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface ISolicitationService
    {
        int GetCountPendingHours(Guid organizationId);
        int GetCountResponsePendingHours();
    }
}
