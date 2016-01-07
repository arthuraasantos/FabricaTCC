using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seedwork.Const;
using TCCPontoEletronico.AppService.Interface;

namespace TCCPontoEletronico.AppService.Entity
{
    public abstract class Notification : INotification
    {
        public void Notify(NotificationType notificationType)
        {
            throw new NotImplementedException();
        }

        public void NotifyWithMessage(NotificationType notificationType, string message)
        {
            throw new NotImplementedException();
        }

        
    }

    public class DefaultNotification: Notification
    {

    }

    public class CEONotification: DefaultNotification
    {
        public void NotifyNewUser(NotificationType notificationType)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void NotifyError(NotificationType notificationType, string message, Exception typeException)
        {
            //TODO Enviar log para e-mails "Marlon e Arthur"

        }
    }
}
