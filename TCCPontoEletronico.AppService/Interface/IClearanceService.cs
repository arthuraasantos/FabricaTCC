

using Dominio.Model;
using System;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IClearanceService
    {
        Folga GetClearanceNotificationWarning(DateTime now, Funcionario employee);

        int GetCountPendingClearance();
        int GetCountResponsePendingClearance();

    }
}
