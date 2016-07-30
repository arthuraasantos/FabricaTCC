
using Dominio.Model;
using Dominio.Repository;
using System;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Entity
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository OrganizationRepository;

        public EmpresaService(IEmpresaRepository organizationRepository)
        {
            OrganizationRepository = organizationRepository;
        }
        public int CountOrganizations() => OrganizationRepository.GetCountOrganizations();

        public EmpresaNovoDto CreateOrganization(string fantasyName, string cnpj)
        {
            EmpresaNovoDto organizationDTO = new EmpresaNovoDto();
            try
            {
                Empresa organization = new Empresa();
                organization.Id = Guid.NewGuid();
                organization.NomeFantasia = fantasyName;
                organization.Cnpj = cnpj;
                OrganizationRepository.Salvar(organization);
                organizationDTO = new EmpresaNovoDto(organization.Id,organization.NomeFantasia);

            }
            catch (Exception ex)
            {
                //ToDo Implementar Log de erros
                throw;
            }

            return organizationDTO;
        }

        public EmpresaNovoDto GetOrganization(Guid id)
        {
            try
            {
                var organization = OrganizationRepository.PesquisarPeloId(id);
                return new EmpresaNovoDto
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
