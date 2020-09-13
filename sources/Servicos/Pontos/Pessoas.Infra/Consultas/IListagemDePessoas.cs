using System.Collections.Generic;
using Pessoas.Dominio.Dtos;

namespace Pessoas.Infra.Consultas
{
    public interface IListagemDePessoas
    {
        List<PessoaDto> Listar();
    }
}