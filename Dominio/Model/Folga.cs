using Seedwork.Const;
using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
    public class Folga: EntityBase
    {
        public virtual Funcionario Funcionario { get; set; }
        public DateTime Data { get; set; }
        public string Justificativa { get; set; }
        public RespostaSolicitacao Resposta { get; set; }
    }
}
