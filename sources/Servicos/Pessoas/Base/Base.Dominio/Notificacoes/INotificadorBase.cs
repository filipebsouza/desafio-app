using System.Collections.Generic;
using Flunt.Notifications;

namespace Base.Dominio.Notificacoes
{
    public interface INotificadorBase
    {
        bool PossuiNotificacoes { get; }
        bool NaoPossuiNotificacoes { get; }
        IReadOnlyCollection<Notification> Notificacoes { get; }
        void Notificar(string notificacao);
        void Notificar(string propriedade, string notificacao);
        void Notificar(IList<Notification> notificacoes);
        void Notificar(IReadOnlyCollection<Notification> notificacoes);
        void Limpar();
    }
}