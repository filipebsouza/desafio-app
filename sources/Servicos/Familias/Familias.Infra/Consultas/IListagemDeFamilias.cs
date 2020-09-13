using System.Collections.Generic;
using Familias.Dominio.Dtos;

namespace Familias.Infra.Consultas
{
    public interface IListagemDeFamilias
    {
        List<FamiliaDto> Listar();
    }
}