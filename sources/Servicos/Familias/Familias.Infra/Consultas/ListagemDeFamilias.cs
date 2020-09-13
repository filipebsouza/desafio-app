using System.Collections.Generic;
using System.Linq;
using Familias.Dominio.Dtos;
using Familias.Dominio.Entidades;
using Familias.Infra.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Familias.Infra.Consultas
{
    public class ListagemDeFamilias : IListagemDeFamilias
    {
        private readonly FamiliaContexto _familiaContexto;

        public ListagemDeFamilias(FamiliaContexto familiaContexto)
        {
            _familiaContexto = familiaContexto;
        }

        public List<FamiliaDto> Listar()
        {
            return _familiaContexto.Set<Familia>()
                .Include(familia => familia.Pessoas)
                .Include(familia => familia.Rendas)
                .Select(familia => new FamiliaDto
                {
                    Id = familia.Id,
                    Pessoas = familia.Pessoas.Select(pessoa => new PessoaDto
                    {
                        Id = pessoa.Id,
                        Nome = pessoa.Nome,
                        DataDeNascimento = pessoa.DataDeNascimento,
                        DescricaoTipoDaPessoa = pessoa.DescricaoTipoDaPessoa
                    }).ToList(),
                    RendaPorPessoas = familia.Rendas.Select(renda => new RendaPorPessoaDto
                    {
                        Id = renda.Id,
                        PessoaId = renda.PessoaId,
                        Valor = renda.Valor
                    }).ToList(),
                })
                .ToList();
        }
    }
}