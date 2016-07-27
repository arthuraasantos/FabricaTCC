using Seedwork.Const;
using System.Collections.Generic;

namespace Infraestrutura.Importacao
{
    public interface IImportacao
    {
        bool ImportarTextoPlanilha(List<string[]> textoPlanilha, string[] relacaoColunas, TipoImportacao tipoImportacao);
    }
}