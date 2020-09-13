using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Familias.Dominio.Entidades;
using Familias.Dominio.Repositorios;
using Familias.Infra.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Familias.Infra.Repositorios
{
    public class FamiliaRepositorio : IFamiliaRepositorio
    {
        private readonly FamiliaContexto _contexto;

        public FamiliaRepositorio(FamiliaContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<Familia> Incluir(Familia rendaPorPessoa)
        {
            await _contexto.Set<Familia>().AddAsync(rendaPorPessoa);

            return rendaPorPessoa;
        }

        public async Task<List<Familia>> ObterFamiliasPorPessoaIds(List<Guid> pessoaIds)
        {
            return await _contexto.Set<Familia>()
                    .Include(familia => familia.Pessoas)
                .Where(familia => familia.Pessoas.Any(pessoa =>
                   pessoaIds.Contains(pessoa.Id)
                ))
                .ToListAsync();
        }

        public async Task<int> Salvar()
        {
            return await _contexto.SaveChangesAsync();
        }
    }
}