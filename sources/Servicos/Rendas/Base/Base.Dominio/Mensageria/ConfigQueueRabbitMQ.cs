using System.Collections.Generic;

namespace Base.Dominio.Mensageria
{
    public class ConfigQueueRabbitMQ : IConfigQueueRabbitMQ
    {
        public string Queue { get; set; } = "main-queue";
        public bool Durable { get; set; } = true;
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
        public string Exchange { get; set; } = "";
        private string routingKey;
        public string RoutingKey
        {
            get
            {
                return routingKey ?? Queue;
            }
            set
            {
                routingKey = value;
            }
        }
    }
}