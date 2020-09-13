namespace Base.Dominio
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