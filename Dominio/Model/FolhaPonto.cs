using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
    public class FolhaPonto : EntityBase
    {
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public Boolean Aberto { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
