using RabbitMQ.Client;
using Base.Dominio.Mensageria;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using RabbitMQ.Client.Events;
using System.Text;

namespace Base.Infra.Mensageria
{
    public class ReceberDaFilaBase<ObjetoRecebidoEmJson> : BackgroundService, IReceberDaFilaBase<ObjetoRecebidoEmJson> 
        where ObjetoRecebidoEmJson : class
    {
        private readonly IModel _canal;
        private readonly IConnection _conexao;
        private readonly ConfigRabbitMQ _configuracao;
        private readonly ConnectionFactory _fabricaDeConexoes;
        private readonly IConfigQueueRabbitMQ _configuracaoParaRecebimento;
        private readonly IServicoDeDominioParaProcessamentoDeFilaBase<ObjetoRecebidoEmJson> _servicoDeDominio;

        public ReceberDaFilaBase(
            IConfigQueueRabbitMQ configuracaoParaRecebimento,
            IServicoDeDominioParaProcessamentoDeFilaBase<ObjetoRecebidoEmJson> servicoDeDominio
        )
        {
            _configuracao = new ConfigRabbitMQ();
            _fabricaDeConexoes = new ConnectionFactory
            {
                HostName = _configuracao.HostName,
                UserName = _configuracao.UserName,
                Password = _configuracao.Password,
                RequestedHeartbeat = _configuracao.RequestedHeartbeat
            };
            _configuracaoParaRecebimento = configuracaoParaRecebimento;
            _servicoDeDominio = servicoDeDominio;

            _conexao = _fabricaDeConexoes.CreateConnection();
            _conexao.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _canal = _conexao.CreateModel();
            _canal.QueueDeclare(
                queue: _configuracaoParaRecebimento.Queue,
                durable: _configuracaoParaRecebimento.Durable,
                exclusive: _configuracaoParaRecebimento.Exclusive,
                autoDelete: _configuracaoParaRecebimento.AutoDelete,
                arguments: _configuracaoParaRecebimento.Arguments
            );
        }

        public void ProcessarMensagem(ObjetoRecebidoEmJson dto)
        {
            _servicoDeDominio.Processar(dto);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumidor = new EventingBasicConsumer(_canal);
            consumidor.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var json = JsonConvert.DeserializeObject<ObjetoRecebidoEmJson>(content);

                ProcessarMensagem(json);

                _canal.BasicAck(ea.DeliveryTag, false);
            };

            consumidor.Shutdown += OnConsumerShutdown;
            consumidor.Registered += OnConsumerRegistered;
            consumidor.Unregistered += OnConsumerUnregistered;
            consumidor.ConsumerCancelled += OnConsumerConsumerCancelled;

            _canal.BasicConsume(
                _configuracaoParaRecebimento.Queue,
                false,
                consumidor
            );

            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _canal.Close();
            _conexao.Close();
            base.Dispose();
        }
    }
}