using System;

namespace Seedwork.Entity
{
    public abstract class EntityBase 
    {
        public Guid Id { get; set; }
        public DateTime Criacao { get; set; }
        public DateTime? Alteracao { get; set; }
        public DateTime? Exclusao { get; set; }

        public EntityBase()
        {
            if (Criacao == null)
                Criacao = DateTime.Today;
        }
    }
}
