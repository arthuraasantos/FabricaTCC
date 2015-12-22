using Seedwork.Const;
using System;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface ICeosNotification
    {
        void NotifyNewUser(NotificationType notificationType);
        void NotifyError(NotificationType notificationType, string message, Exception typeException);

    }
}
