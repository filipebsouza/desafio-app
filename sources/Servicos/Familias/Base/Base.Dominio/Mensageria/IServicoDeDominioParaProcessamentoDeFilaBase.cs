using System.Threading.Tasks;

namespace Base.Dominio.Mensageria
{
    public interface IServicoDeDominioParaProcessamentoDeFilaBase<Dto> where Dto : class
    {
        Task Processar(Dto dto);
    }
}