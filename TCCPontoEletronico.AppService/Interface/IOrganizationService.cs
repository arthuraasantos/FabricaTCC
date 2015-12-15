
using Dominio.Model;
using System;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IOrganizationService
    {
        int CountOrganizations();
        OrganizationNewDTO CreateOrganization(string fantasyName);
        Guid GetOrganizationId(string organizationName);
        OrganizationNewDTO GetOrganization(Guid Id);
    }
}
