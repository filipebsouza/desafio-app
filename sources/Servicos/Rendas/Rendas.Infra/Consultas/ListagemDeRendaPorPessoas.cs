using System.Collections.Generic;
using System.Linq;
using Rendas.Dominio.Dtos;
using Rendas.Infra.Contextos;

namespace Rendas.Infra.Consultas
{
    public class ListagemDeRendaPorPessoas : IListagemDeRendaPorPessoas
    {
        private readonly RendaPorPessoaContexto _rendaPorPessoaContexto;

        public ListagemDeRendaPorPessoas(RendaPorPessoaContexto rendaPorPessoaContexto)
        {
            _rendaPorPessoaContexto = rendaPorPessoaContexto;
        }

        public List<RendaPorPessoaDto> Listar()
        {
            return _rendaPorPessoaContexto.RendaPorPessoas
                .Select(p => new RendaPorPessoaDto
                {
                    Id = p.Id,
                    PessoaId = p.PessoaId,
                    NomePessoa = p.NomePessoa,
                    Valor = p.Valor
                })
                .ToList();
        }
    }
}