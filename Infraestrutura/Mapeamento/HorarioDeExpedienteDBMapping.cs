using Dominio.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Mapeamento
{
    public class HorarioDeExpedienteDBMapping: EntityTypeConfiguration<HorarioDeExpediente>
    {
        public HorarioDeExpedienteDBMapping()
        {
            HasKey(p => p.Id);
            
        }
    }
}
