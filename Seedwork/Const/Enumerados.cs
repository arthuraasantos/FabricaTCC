using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedwork.Const
{
    public enum PerfilAcesso
    {
        Administrador,
        Gerente,
        Funcionario
    }

    public enum TipoSolicitacao
    {
        Nenhum,
        Ajuste,
        Inclusao,
        Desconsideracao,
        PreAssinada
    };
    public enum RespostaSolicitacao
    {
        Nenhuma,
        Aprovado,
        Reprovado
    }

    public enum TypeResponse
    {
        Success,
        Error
    }

}
