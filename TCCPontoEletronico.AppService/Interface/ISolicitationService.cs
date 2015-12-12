using System;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface ISolicitationService
    {
        int GetCountPendingHours(Guid organizationLogged);
    }
}
