using System.Collections.Generic;

namespace Base.Dominio.Mensageria
{
    public interface IConfigQueueRabbitMQ
    {
        string Queue { get; set; }
        bool Durable { get; set; }
        bool Exclusive { get; set; }
        bool AutoDelete { get; set; }
        IDictionary<string, object> Arguments { get; set; }
        string Exchange { get; set; }
        string RoutingKey { get; set; }
    }
}