using System.Threading.Tasks;
using Pontos.Dominio.Dtos;

namespace Pontos.Dominio.Servicos
{
    public interface IArmazenadorDePontuacaoPorFamilia
    {
        Task<FamiliaDto> Armazenar(FamiliaDto dto);
    }
}