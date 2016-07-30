
using Dominio.Model;
using Dominio.Repository;
using System;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Entity
{
    public class HorarioDeExpedienteService : IHorarioDeExpedienteService
    {
        private readonly IHorarioDeExpedienteRepository _horarioDeExpedienteRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEmpresaService _empresaService;

        public HorarioDeExpedienteService(IHorarioDeExpedienteRepository officeHoursRepository, IEmpresaService organizationService, IEmpresaRepository empresaRepository)
        {
            _horarioDeExpedienteRepository = officeHoursRepository;
            _empresaRepository = empresaRepository;
            _empresaService = organizationService;
        }
        public HorarioDeExpedienteDto CreateForLogin(Guid idEmpresa)
        {
            try
            {
                HorarioDeExpediente newOfficeHour = new HorarioDeExpediente();

                newOfficeHour.Empresa = new Empresa();
                newOfficeHour.Id = Guid.NewGuid();
                newOfficeHour.Empresa = _empresaRepository.PesquisarPeloId(idEmpresa);
                newOfficeHour.Descricao = "Horário padrão";
                newOfficeHour.NumeroHorasPorDia = 8;

                _horarioDeExpedienteRepository.Salvar(newOfficeHour);
                return new HorarioDeExpedienteDto
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
