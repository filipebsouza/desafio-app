using Base.Dominio.Mensageria;
using Base.Dominio.Notificacoes;
using Rendas.Dominio.Dtos;

namespace Rendas.Dominio.Servicos
{
    public class AlteradorDePontosPorInsercaoDeRenda : ServicoDeDominioBase, IAlteradorDePontosPorInsercaoDeRenda
    {
        private readonly string nomeDaFila = "Servico_Pontos.API";
        private readonly ConfigSendMessageRabbitMQ _configuracaoDeEnvioDeMensagem;
        private readonly IEnviarParaFilaBase<RendaPorPessoaDto> _enviarRendaParaPontuacao;

        public AlteradorDePontosPorInsercaoDeRenda(
            INotificadorBase notificador,
            ConfigSendMessageRabbitMQ configuracaoDeEnvioDeMensagem,
            IEnviarParaFilaBase<RendaPorPessoaDto> enviarRendaParaPontuacao
        ) : base(notificador)
        {
            _configuracaoDeEnvioDeMensagem = new ConfigSendMessageRabbitMQ
            {
                Queue = nomeDaFila
            };
            _enviarRendaParaPontuacao = enviarRendaParaPontuacao;
        }

        public void Alterar(RendaPorPessoaDto dto)
        {
            _enviarRendaParaPontuacao.EnviarParaFila(_configuracaoDeEnvioDeMensagem, dto);
        }
    }
}