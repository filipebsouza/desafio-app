namespace Base.Dominio.Mensageria
{
    public interface IEnviarParaFilaBase<ObjetoParaEnvioEmJson> where ObjetoParaEnvioEmJson : class
    {
        void EnviarParaFila(IConfigQueueRabbitMQ configuracaoDeEnvio, ObjetoParaEnvioEmJson objetoParaEnvioEmJson);
    }
}