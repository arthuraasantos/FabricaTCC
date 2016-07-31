using Dominio.Model;
using Infraestrutura.Mapeamento.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Mapeamento
{
    internal class ItemHorarioDeExpedienteDBMapping: BaseDbMapping<ItemHorarioDeExpediente>
    {
        public ItemHorarioDeExpedienteDBMapping()
        {
            HasKey(p => p.Id);
            HasRequired(p => p.HorarioDeExpediente);
        }
    }
}
