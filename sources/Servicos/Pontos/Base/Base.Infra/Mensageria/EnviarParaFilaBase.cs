using System.Text;
using Base.Dominio.Mensageria;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Base.Infra.Mensageria
{
    public class EnviarParaFilaBase<ObjetoParaEnvioEmJson> : IEnviarParaFilaBase<ObjetoParaEnvioEmJson>
        where ObjetoParaEnvioEmJson : class
    {
        private readonly ConfigRabbitMQ _configuracao;
        private readonly ConnectionFactory _fabricaDeConexoes;

        public EnviarParaFilaBase(ConfigRabbitMQ configuracao, ConnectionFactory fabricaDeConexoes)
        {
            _configuracao = configuracao;
            _fabricaDeConexoes = new ConnectionFactory
            {
                HostName = _configuracao.HostName,
                UserName = _configuracao.UserName,
                Password = _configuracao.Password
            };
        }

        public void EnviarParaFila(ConfigSendMessageRabbitMQ configuracaoDeEnvio, ObjetoParaEnvioEmJson objetoParaEnvioEmJson)
        {
            try
            {
                using (var connection = _fabricaDeConexoes.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: configuracaoDeEnvio.Queue,
                        durable: configuracaoDeEnvio.Durable,
                        exclusive: configuracaoDeEnvio.Exclusive,
                        autoDelete: configuracaoDeEnvio.AutoDelete,
                        arguments: configuracaoDeEnvio.Arguments
                    );

                    var json = JsonConvert.SerializeObject(objetoParaEnvioEmJson);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(
                        exchange: configuracaoDeEnvio.Exchange,
                        routingKey: configuracaoDeEnvio.RoutingKey,
                        basicProperties: null,
                        body: body
                    );
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}