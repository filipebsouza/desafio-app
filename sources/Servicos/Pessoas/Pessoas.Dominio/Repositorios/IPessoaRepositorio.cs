using System.Threading.Tasks;
using Pessoas.Dominio.Entidades;

namespace Pessoas.Dominio.Repositorios
{
    public interface IPessoaRepositorio
    {
        Task<Pessoa> Incluir(Pessoa pessoa);
        Task<int> Salvar();
    }
}