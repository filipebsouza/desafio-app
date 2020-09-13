using System.Collections.Generic;
using Rendas.Dominio.Dtos;

namespace Rendas.Infra.Consultas
{
    public interface IListagemDeRendaPorPessoas
    {
        List<RendaPorPessoaDto> Listar();
    }
}