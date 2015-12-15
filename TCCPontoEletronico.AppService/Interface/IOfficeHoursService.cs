
using System;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IOfficeHoursService
    {
        OfficeHoursDTO CreateForLogin(Guid organizationId);
    }
}
