namespace Base.Dominio.Notificacoes
{
    public class ServicoDeDominioBase
    {
        public readonly INotificadorBase Notificador;
        public ServicoDeDominioBase(INotificadorBase notificador)
        {
            Notificador = notificador;
        }
    }
}