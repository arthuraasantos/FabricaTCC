using Seedwork.Const;
using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
    public class Solicitacao : EntityBase
    {
        public virtual Ponto Ponto { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public DateTime DataHora { get; set; }
        public String Justificativa { get; set; }
        public TipoSolicitacao Tipo { get; set; }
        public RespostaSolicitacao Resposta { get; set; }

    }
}
