using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrontEnd.Controllers
{
    public class HorarioDeExpedienteToHorarioDeExpedienteEditar : ConversorBase<HorarioDeExpediente, HorarioDeExpedienteEditar>
    {
        public IEmpresaRepository EmpresaRepository;

        
        public HorarioDeExpedienteToHorarioDeExpedienteEditar(IEmpresaRepository empresaRepository)
        {

        }


        public override void AplicarValores(HorarioDeExpediente origem, HorarioDeExpedienteEditar destino)
        {
            destino.Descricao = origem.Descricao;
            destino.NumeroHorasPorDia = origem.NumeroHorasPorDia;
            destino.IdEmpresa = origem.Empresa.Id;
        }
        public override void AplicarValores(HorarioDeExpedienteEditar origem, HorarioDeExpediente destino)
        {
            destino.Descricao = origem.Descricao;
            destino.NumeroHorasPorDia = origem.NumeroHorasPorDia;
        }
    }
}
