namespace Base.Infra.Mensageria
{
    public class ConfigRabbitMQ
    {
        public ConfigRabbitMQ()
        { }

        public string HostName { get; set; } = "localhost";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string Queue { get; set; } = "main-queue";
        public System.TimeSpan RequestedHeartbeat { get; set; } = new System.TimeSpan(10);
    }
}