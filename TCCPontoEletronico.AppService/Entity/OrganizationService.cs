
using Dominio.Model;
using Dominio.Repository;
using System;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;

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

        public OrganizationNewDTO CreateOrganization(string fantasyName)
        {
            OrganizationNewDTO organizationDTO = new OrganizationNewDTO();
            try
            {
                Empresa organization = new Empresa();
                organization.Id = Guid.NewGuid();
                organization.NomeFantasia = fantasyName;
                OrganizationRepository.Salvar(organization);
                organizationDTO = new OrganizationNewDTO(organization.Id,organization.NomeFantasia);

            }
            catch (Exception)
            {
                //ToDo Implementar Log de erros
                throw;
            }

            return organizationDTO;
        }

        public OrganizationNewDTO GetOrganization(Guid id)
        {
            try
            {
                var organization = OrganizationRepository.PesquisarPeloId(id);
                return new OrganizationNewDTO
                {
                    Id = organization.Id,
                    Name = organization.NomeFantasia
                };

            }
            catch (Exception)
            {
                //ToDo Implementar log de erro
                throw;
            }
            
        }

        public Guid GetOrganizationId(string organizationName) => OrganizationRepository.GetOrganizationId(organizationName);
        
    }
}
