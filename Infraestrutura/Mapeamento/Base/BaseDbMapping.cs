using Seedwork.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Infraestrutura.Mapeamento.Base
{
    internal abstract class BaseDbMapping<T>: EntityTypeConfiguration<T>
        where T : EntityBase
    {
        public BaseDbMapping()
        {
            Property(p => p.Criacao).IsRequired();
            Property(p => p.Alteracao);
            Property(p => p.Exclusao);
        }
    }
}
