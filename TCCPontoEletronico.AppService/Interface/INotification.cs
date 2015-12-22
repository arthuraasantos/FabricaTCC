using Seedwork.Const;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface INotification
    {
        void Notify(NotificationType notificationType);
        void NotifyWithMessage(NotificationType notificationType, string message);
    }
}
