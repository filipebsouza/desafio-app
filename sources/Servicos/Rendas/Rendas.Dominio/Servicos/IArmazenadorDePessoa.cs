using System.Threading.Tasks;
using Rendas.Dominio.Dtos;

namespace Rendas.Dominio.Servicos
{
    public interface IArmazenadorDeRendaPorPessoa
    {
        Task<RendaPorPessoaDto> Armazenar(RendaPorPessoaDto dto);
    }
}