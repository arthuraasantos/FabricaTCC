
using Dominio.Model;
using Dominio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Entity
{
    public class OfficeHoursService : IOfficeHoursService
    {
        private readonly IHorarioDeExpedienteRepository OfficeHoursRepository;
        private readonly IOrganizationService OrganizationService;

        public OfficeHoursService(IHorarioDeExpedienteRepository officeHoursRepository, IOrganizationService organizationService)
        {
            OfficeHoursRepository = officeHoursRepository;
            OrganizationService = organizationService;
        }
        public OfficeHoursDTO CreateForLogin(Guid organizationId)
        {
            try
            {
                HorarioDeExpediente newOfficeHour = new HorarioDeExpediente();

                var organization = OrganizationService.GetOrganization(organizationId);
                newOfficeHour.Empresa = new Empresa();
                //newOfficeHour.Empresa = new Empresa { Id = organization.Id, NomeFantasia = organization.Name } ;
                newOfficeHour.Empresa.Id = organization.Id;
                newOfficeHour.Descricao = "Horário padrão";
                newOfficeHour.NumeroHorasPorDia = 8;

                OfficeHoursRepository.Salvar(newOfficeHour);
                return new OfficeHoursDTO
                {
                    Id = newOfficeHour.Id,
                    Description = newOfficeHour.Descricao
                };
            }
            catch (Exception)
            {
                //ToDo Implementar log de erro
                throw;
            }

        }
    }
}
