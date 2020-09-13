using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Familias.Dominio.Entidades;

namespace Familias.Dominio.Repositorios
{
    public interface IFamiliaRepositorio
    {
        Task<Familia> Incluir(Familia rendaPorPessoa);
        Task<int> Salvar();
        Task<List<Familia>> ObterFamiliasPorPessoaIds(List<Guid> pessoaIds);
    }
}