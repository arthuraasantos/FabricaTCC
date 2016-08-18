
using System;
using System.Data.Entity;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IHorarioDeExpedienteService
    {
        HorarioDeExpedienteDto Create(Guid idEmpresa, String Descricao, DbContextTransaction Transacao = null);
    }
}
