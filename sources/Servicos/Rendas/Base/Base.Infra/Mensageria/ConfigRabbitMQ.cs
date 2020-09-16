namespace Base.Infra.Mensageria
{
    public class ConfigRabbitMQ
    {
        public string HostName { get; set; } = "localhost";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string Queue { get; set; } = "main-queue";
    }
}