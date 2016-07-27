using Seedwork.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IImportacaoService
    {
        bool ImportarTextoPlanilha(List<string[]> textoPlanilha, string[] relacaoColunas, TipoImportacao tipoImportacao);
    }
}
