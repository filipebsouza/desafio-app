using System.Threading.Tasks;
using Rendas.Dominio.Entidades;

namespace Rendas.Dominio.Repositorios
{
    public interface IRendaPorPessoaRepositorio
    {
        Task<RendaPorPessoa> Incluir(RendaPorPessoa rendaPorPessoa);
        Task<int> Salvar();
    }
}