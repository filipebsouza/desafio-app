using System.Threading.Tasks;
using Pontos.Dominio.Entidades;

namespace Pontos.Dominio.Repositorios
{
    public interface IPontuacaoPorFamiliaRepositorio
    {
        Task<PontuacaoPorFamilia> Incluir(PontuacaoPorFamilia pontuacaoPorFamilia);
        Task<int> Salvar();
    }
}