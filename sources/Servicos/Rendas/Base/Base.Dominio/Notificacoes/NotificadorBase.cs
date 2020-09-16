using System.Collections.Generic;
using Flunt.Notifications;

namespace Base.Dominio.Notificacoes
{
    public class NotificadorBase : Notifiable, INotificadorBase
    {
        public NotificadorBase() { }

        public bool PossuiNotificacoes
        {
            get
            {
                return Notifications.Count > 0;
            }
        }
        public bool NaoPossuiNotificacoes
        {
            get
            {
                return Notifications.Count <= 0;
            }
        }
        public IReadOnlyCollection<Notification> Notificacoes
        {
            get
            {
                return Notifications;
            }
        }
        public void Notificar(string notificacao) => AddNotification("Sem nome de propriedade", notificacao);
        public void Notificar(string propriedade, string notificacao) => AddNotification(propriedade, notificacao);
        public void Notificar(IList<Notification> notificacoes) => AddNotifications(notificacoes);
        public void Notificar(IReadOnlyCollection<Notification> notificacoes) => AddNotifications(notificacoes);
        public void Limpar() => Clear();
    }
}