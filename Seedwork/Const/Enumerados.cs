using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedwork.Const
{
    
        public enum TipoSolicitacao {
            Nenhum,             
            Ajuste,             
            Inclusao,           
            Desconsideracao,    
            PreAssinada         
        };
        public enum TipoJustificativa { 
            SemMotivo,
            Ferias,
            Folgas,
            OutrosComprovados
        }

}
