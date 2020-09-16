using System.Collections.Generic;
using Base.Dominio.Mensageria;

namespace Pontos.Infra.Mensageria
{
    public class ConfigQueueServicoPontosAPI : IConfigQueueRabbitMQ
    {
        public ConfigQueueServicoPontosAPI()
        { }

        public string Queue { get; set; } = "Servico_Pontos.API";
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