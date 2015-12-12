
using Dominio.Repository;
using System;
using TCCPontoEletronico.AppService.Interface;

namespace TCCPontoEletronico.AppService.Entity
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IEmpresaRepository OrganizationRepository;

        public OrganizationService(IEmpresaRepository organizationRepository)
        {
            OrganizationRepository = organizationRepository;
        }
        public int CountOrganizations() => OrganizationRepository.GetCountOrganizations();
            
    }
}
