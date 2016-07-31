
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
        private readonly IItemHorarioDeExpedienteRepository ItemHorarioRepository;

        public OfficeHoursService(IHorarioDeExpedienteRepository officeHoursRepository, IOrganizationService organizationService, IItemHorarioDeExpedienteRepository itemHorarioRepository)
        {
            OfficeHoursRepository = officeHoursRepository;
            OrganizationService = organizationService;
            ItemHorarioRepository = itemHorarioRepository;
        }
        public OfficeHoursDTO CreateForLogin(Guid organizationId)
        {
            try
            {
                HorarioDeExpediente newOfficeHour = new HorarioDeExpediente();

                //var organization = OrganizationService.GetOrganization(organizationId);
                newOfficeHour.Empresa = new Empresa();
                newOfficeHour.Id = Guid.NewGuid();
                newOfficeHour.Empresa.Id = organizationId;
                newOfficeHour.Descricao = "Horário padrão";

                OfficeHoursRepository.Salvar(newOfficeHour);

                #region 'Itens de Hora de expediente 0'

                ItemHorarioDeExpediente itemHorarioPadrao0 = new ItemHorarioDeExpediente()
                {
                    Id = Guid.NewGuid(),
                    DiaSemana = 0,
                    Horas = 0,
                    HorarioDeExpediente = newOfficeHour,
                    Criacao = DateTime.Now.Date
                };

                ItemHorarioRepository.Salvar(itemHorarioPadrao0);

                #endregion
                #region 'Itens de Hora de expediente 1'

                ItemHorarioDeExpediente itemHorarioPadrao1 = new ItemHorarioDeExpediente()
                {
                    Id = Guid.NewGuid(),
                    DiaSemana = 1,
                    Horas = 8,
                    HorarioDeExpediente = newOfficeHour,
                    Criacao = DateTime.Now.Date
                };

                ItemHorarioRepository.Salvar(itemHorarioPadrao1);

                #endregion
                #region 'Itens de Hora de expediente 2'

                ItemHorarioDeExpediente itemHorarioPadrao2 = new ItemHorarioDeExpediente()
                {
                    Id = Guid.NewGuid(),
                    DiaSemana = 2,
                    Horas = 8,
                    HorarioDeExpediente = newOfficeHour,
                    Criacao = DateTime.Now.Date
                };

                ItemHorarioRepository.Salvar(itemHorarioPadrao2);

                #endregion
                #region 'Itens de Hora de expediente 3'

                ItemHorarioDeExpediente itemHorarioPadrao3 = new ItemHorarioDeExpediente()
                {
                    Id = Guid.NewGuid(),
                    DiaSemana = 3,
                    Horas = 8,
                    HorarioDeExpediente = newOfficeHour,
                    Criacao = DateTime.Now.Date
                };

                ItemHorarioRepository.Salvar(itemHorarioPadrao3);

                #endregion
                #region 'Itens de Hora de expediente 4'

                ItemHorarioDeExpediente itemHorarioPadrao4 = new ItemHorarioDeExpediente()
                {
                    Id = Guid.NewGuid(),
                    DiaSemana = 4,
                    Horas = 8,
                    HorarioDeExpediente = newOfficeHour,
                    Criacao = DateTime.Now.Date
                };

                ItemHorarioRepository.Salvar(itemHorarioPadrao4);

                #endregion
                #region 'Itens de Hora de expediente 5'

                ItemHorarioDeExpediente itemHorarioPadrao5 = new ItemHorarioDeExpediente()
                {
                    Id = Guid.NewGuid(),
                    DiaSemana = 5,
                    Horas = 8,
                    HorarioDeExpediente = newOfficeHour,
                    Criacao = DateTime.Now.Date
                };

                ItemHorarioRepository.Salvar(itemHorarioPadrao5);

                #endregion
                #region 'Itens de Hora de expediente 6'

                ItemHorarioDeExpediente itemHorarioPadrao6 = new ItemHorarioDeExpediente()
                {
                    Id = Guid.NewGuid(),
                    DiaSemana = 6,
                    Horas = 0,
                    HorarioDeExpediente = newOfficeHour,
                    Criacao = DateTime.Now.Date
                };

                ItemHorarioRepository.Salvar(itemHorarioPadrao6);

                #endregion

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
