using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Dominio.Mensageria;
using Base.Infra.Mensageria;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Pontos.Dominio.Dtos;
using Pontos.Dominio.Servicos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Pontos.Infra.Mensageria
{
    public class ReceberDaFila : BackgroundService
    {
        private readonly IModel _canal;
        private readonly IConnection _conexao;
        private readonly ConfigRabbitMQ _configuracao;
        private readonly ConnectionFactory _fabricaDeConexoes;
        private readonly ConfigQueueServicoPontosAPI _configuracaoParaRecebimento;        

        public ReceberDaFila()
        {
            _configuracao = new ConfigRabbitMQ();
            _fabricaDeConexoes = new ConnectionFactory
            {
                HostName = _configuracao.HostName,
                UserName = _configuracao.UserName,
                Password = _configuracao.Password,
                RequestedHeartbeat = _configuracao.RequestedHeartbeat
            };
            _configuracaoParaRecebimento = new ConfigQueueServicoPontosAPI();

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

        public void ProcessarMensagem(RendaPorPessoaDto dto)
        {
            // TODO: Chamar serviço de alteração            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumidor = new EventingBasicConsumer(_canal);
            consumidor.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var json = JsonConvert.DeserializeObject<RendaPorPessoaDto>(content);

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