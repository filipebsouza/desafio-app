using System.Threading.Tasks;
using Pessoas.Dominio.Dtos;

namespace Pessoas.Dominio.Servicos
{
    public interface IArmazenadorDePessoa
    {
        Task<PessoaDto> Armazenar(PessoaDto dto);
    }
}