
using Dominio.Model;
using System;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IEmpresaService
    {
        int CountOrganizations();
        EmpresaNovoDto CreateOrganization(string fantasyName,string cnpj);
        Guid GetOrganizationId(string organizationName);
        EmpresaNovoDto GetOrganization(Guid Id);
    }
}
