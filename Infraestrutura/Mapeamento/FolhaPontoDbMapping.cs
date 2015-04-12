using Dominio.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Mapeamento
{
    public class FolhaPontoDbMapping : EntityTypeConfiguration<FolhaPonto>
    {
        public FolhaPontoDbMapping()
        {
            HasKey(p => p.Id);
            HasRequired(p => p.Empresa);
        }
    }
}
