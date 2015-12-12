
using Dominio.Model;
using System;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IVacationService
    {
        Ferias GetVacationNotificationWarning(DateTime now, Funcionario employee);

        int GetCountVacationPending();
        int GetCountResponsePendingVacation();

    }
}
