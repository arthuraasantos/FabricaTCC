using Dominio.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Mapeamento
{
    public class SolicitacaoDbMapping : EntityTypeConfiguration<Solicitacao>
    {
        public SolicitacaoDbMapping()
        {
            HasKey(p => p.Id);
            HasRequired(p => p.Ponto);
        }
    }
}
