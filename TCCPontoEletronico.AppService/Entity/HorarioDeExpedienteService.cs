
using Dominio.Model;
using Dominio.Repository;
using Infraestrutura;
using System;
using System.Data.Entity;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Entity
{
    public class HorarioDeExpedienteService : IHorarioDeExpedienteService
    {
        private PontoContext Contexto;

        private readonly IHorarioDeExpedienteRepository _horarioDeExpedienteRepository;
        private readonly IItemHorarioDeExpedienteRepository _itemHorarioDeExpedienteRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEmpresaService _empresaService;

        public HorarioDeExpedienteService(
            IHorarioDeExpedienteRepository officeHoursRepository,
            IItemHorarioDeExpedienteRepository itemHorarioDeExpedienteRepository,
            IEmpresaService organizationService,
            IEmpresaRepository empresaRepository,
            PontoContext contexto)
        {
            Contexto = contexto;
            _horarioDeExpedienteRepository = officeHoursRepository;
            _itemHorarioDeExpedienteRepository = itemHorarioDeExpedienteRepository;
            _empresaRepository = empresaRepository;
            _empresaService = organizationService;
        }

        public HorarioDeExpedienteDto Create(Guid idEmpresa, String Descricao, DbContextTransaction Transacao = null)
        {
            //var UsingTransactionLocal = false;
            //if (Transacao == null)
            //{
            //    Transacao = Contexto.Database.BeginTransaction();
            //    UsingTransactionLocal = true;
            //}

            //using (Transacao)
                try
                {
                    HorarioDeExpediente newOfficeHour = new HorarioDeExpediente();

                    newOfficeHour.Empresa = new Empresa();
                    newOfficeHour.Id = Guid.NewGuid();
                    newOfficeHour.Empresa = _empresaRepository.PesquisarPeloId(idEmpresa);
                    newOfficeHour.Descricao = Descricao;

                    _horarioDeExpedienteRepository.Salvar(newOfficeHour);

                    ItemHorarioDeExpediente newItemOfficeHour0 = new ItemHorarioDeExpediente
                    {
                        HorarioDeExpediente = newOfficeHour,
                        DiaSemana = 0,
                        Horas = 0,
                        Id = Guid.NewGuid()
                    };
                    _itemHorarioDeExpedienteRepository.Salvar(newItemOfficeHour0);

                    ItemHorarioDeExpediente newItemOfficeHour1 = new ItemHorarioDeExpediente
                    {
                        HorarioDeExpediente = newOfficeHour,
                        DiaSemana = 1,
                        Horas = 8,
                        Id = Guid.NewGuid()
                    };
                    _itemHorarioDeExpedienteRepository.Salvar(newItemOfficeHour1);

                    ItemHorarioDeExpediente newItemOfficeHour2 = new ItemHorarioDeExpediente
                    {
                        HorarioDeExpediente = newOfficeHour,
                        DiaSemana = 2,
                        Horas = 8,
                        Id = Guid.NewGuid()
                    };
                    _itemHorarioDeExpedienteRepository.Salvar(newItemOfficeHour2);

                    ItemHorarioDeExpediente newItemOfficeHour3 = new ItemHorarioDeExpediente
                    {
                        HorarioDeExpediente = newOfficeHour,
                        DiaSemana = 3,
                        Horas = 8,
                        Id = Guid.NewGuid()
                    };
                    _itemHorarioDeExpedienteRepository.Salvar(newItemOfficeHour3);

                    ItemHorarioDeExpediente newItemOfficeHour4 = new ItemHorarioDeExpediente
                    {
                        HorarioDeExpediente = newOfficeHour,
                        DiaSemana = 4,
                        Horas = 8,
                        Id = Guid.NewGuid()
                    };
                    _itemHorarioDeExpedienteRepository.Salvar(newItemOfficeHour4);

                    ItemHorarioDeExpediente newItemOfficeHour5 = new ItemHorarioDeExpediente
                    {
                        HorarioDeExpediente = newOfficeHour,
                        DiaSemana = 5,
                        Horas = 8,
                        Id = Guid.NewGuid()
                    };
                    _itemHorarioDeExpedienteRepository.Salvar(newItemOfficeHour5);

                    ItemHorarioDeExpediente newItemOfficeHour6 = new ItemHorarioDeExpediente
                    {
                        HorarioDeExpediente = newOfficeHour,
                        DiaSemana = 6,
                        Horas = 0,
                        Id = Guid.NewGuid()
                    };
                    _itemHorarioDeExpedienteRepository.Salvar(newItemOfficeHour6);

                    //if (UsingTransactionLocal)
                    //{
                    //    Transacao.Commit();
                    //}
                    
                    return new HorarioDeExpedienteDto
                    {
                        Id = newOfficeHour.Id,
                        Description = newOfficeHour.Descricao
                    };

                }
                catch (Exception)
                {
                    //ToDo Implementar log de erro
                    //if (UsingTransactionLocal)
                    //{
                    //    Transacao.Rollback();
                    //}
                    throw;
                }

        }
    }
}
