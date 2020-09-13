using System.Threading.Tasks;
using Familias.Dominio.Dtos;

namespace Familias.Dominio.Servicos
{
    public interface IArmazenadorDeFamilia
    {
        Task<FamiliaDto> Armazenar(FamiliaDto dto);
    }
}