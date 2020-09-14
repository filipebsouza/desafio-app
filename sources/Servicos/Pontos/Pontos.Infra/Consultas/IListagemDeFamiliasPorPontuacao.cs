using System.Collections.Generic;
using Pontos.Dominio.Dtos;

namespace Pontos.Infra.Consultas
{
    public interface IListagemDeFamiliasPorPontuacao
    {
        List<FamiliaDto> Listar();
    }
}