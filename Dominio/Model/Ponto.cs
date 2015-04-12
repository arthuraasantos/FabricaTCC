using Seedwork.Const;
using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
    public class Ponto : EntityBase
    {
        public virtual FolhaPonto FolhaPonto { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public DateTime? DataMarcacao { get; set; }
        public DateTime DataValida { get; set; }
    }
}
