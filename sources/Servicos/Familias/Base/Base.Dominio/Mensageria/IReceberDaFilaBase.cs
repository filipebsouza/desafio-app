namespace Base.Dominio.Mensageria
{
    public interface IReceberDaFilaBase<ObjetoRecebidoEmJson> where ObjetoRecebidoEmJson : class
    {
        void ProcessarMensagem(ObjetoRecebidoEmJson dto);
    }
}