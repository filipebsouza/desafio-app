using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pontos.Dominio.Dtos;
using Pontos.Dominio.Entidades;
using Pontos.Infra.Contextos;

namespace Pontos.Infra.Consultas
{
    public class ListagemDeFamiliasPorPontuacao : IListagemDeFamiliasPorPontuacao
    {
        private readonly PontoContexto _pontoContexto;

        public ListagemDeFamiliasPorPontuacao(PontoContexto pontoContexto)
        {
            _pontoContexto = pontoContexto;
        }

        public List<FamiliaDto> Listar()
        {
            return _pontoContexto.Set<PontuacaoPorFamilia>()
                .Include(pontuacaoPorFamilia => pontuacaoPorFamilia.Familia)
                    .ThenInclude(familia => familia.Pessoas)
                .Include(pontuacaoPorFamilia => pontuacaoPorFamilia.Familia)
                    .ThenInclude(familia => familia.Rendas)
                .AsEnumerable()
                .OrderByDescending(pontuacaoPorFamilia => pontuacaoPorFamilia.TotalDePontos)
                .Select(pontuacaoPorFamilia => new FamiliaDto
                {
                    Id = pontuacaoPorFamilia.Familia.Id,
                    Pessoas = pontuacaoPorFamilia.Familia.Pessoas.Select(pessoa => new PessoaDto
                    {
                        Id = pessoa.Id,
                        Nome = pessoa.Nome,
                        DataDeNascimento = pessoa.DataDeNascimento,
                        TipoDaPessoa = (int)pessoa.TipoDaPessoa
                    })
                    .ToList(),
                    RendaPorPessoas = pontuacaoPorFamilia.Familia.Rendas.Select(rendaPorPessoa => new RendaPorPessoaDto
                    {
                        Id = rendaPorPessoa.Id,
                        PessoaId = rendaPorPessoa.PessoaId,
                        Valor = rendaPorPessoa.Valor
                    })
                    .ToList(),
                    Status = (int)pontuacaoPorFamilia.Familia.Status
                })
                .ToList();
        }
    }
}