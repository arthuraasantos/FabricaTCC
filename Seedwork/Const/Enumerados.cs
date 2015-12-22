
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

    public enum NotificationType
    {
        Information,
        Error,
        Success,
        CEOs,
        User
    }
}
