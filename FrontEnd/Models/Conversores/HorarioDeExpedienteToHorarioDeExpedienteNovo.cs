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
    public class HorarioDeExpedienteToHorarioDeExpedienteNovo: ConversorBase<HorarioDeExpediente,HorarioDeExpedienteNovo>
    {
        public IEmpresaRepository EmpresaRepository;
        public HorarioDeExpedienteToHorarioDeExpedienteNovo(IEmpresaRepository empresaRepository)
        {
            EmpresaRepository = empresaRepository;
        }

        public override void AplicarValores(HorarioDeExpediente origem, HorarioDeExpedienteNovo destino)
        {
            destino.Descricao = origem.Descricao;
            destino.NumeroHorasPorDia = origem.NumeroHorasPorDia;
           
        }

        public override void AplicarValores(HorarioDeExpedienteNovo origem, HorarioDeExpediente destino)
        {
            destino.Descricao = origem.Descricao;
            destino.NumeroHorasPorDia = origem.NumeroHorasPorDia;
            destino.Empresa = EmpresaRepository.PesquisarPeloId(origem.IdEmpresa);
        }
    }
}
