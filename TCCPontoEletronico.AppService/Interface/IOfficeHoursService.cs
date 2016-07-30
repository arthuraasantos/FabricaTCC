
using System;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IHorarioDeExpedienteService
    {
        HorarioDeExpedienteDto CreateForLogin(Guid organizationId);
    }
}
