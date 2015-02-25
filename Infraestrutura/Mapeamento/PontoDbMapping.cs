using Dominio;
using Dominio.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Mapeamento
{
    public class PontoDbMapping : EntityTypeConfiguration<Ponto>
    {
        public PontoDbMapping()
        {
            HasKey(p => p.Id);
            HasRequired(p => p.Funcionario);
        }
    }
}
