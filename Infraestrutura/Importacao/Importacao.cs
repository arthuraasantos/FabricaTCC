using Seedwork.Const;
using System.Collections.Generic;

namespace Infraestrutura.Importacao
{
    public abstract class Importacao : IImportacao
    {
        abstract public bool ImportarTextoPlanilha(List<string[]> textoPlanilha, string[] relacaoColunas, TipoImportacao tipoImportacao);
        
    }
}
