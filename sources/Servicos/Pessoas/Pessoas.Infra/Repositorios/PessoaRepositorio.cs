using System.Threading.Tasks;
using Pessoas.Dominio.Entidades;
using Pessoas.Dominio.Repositorios;
using Pessoas.Infra.Contestos;

namespace Pessoas.Infra.Repositorios
{
    public class PessoaRepositorio : IPessoaRepositorio
    {
        private readonly PessoaContexto _contexto;

        public PessoaRepositorio(PessoaContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<Pessoa> Incluir(Pessoa pessoa)
        {
            await _contexto.Set<Pessoa>().AddAsync(pessoa);

            return pessoa;
        }

        public async Task<int> Salvar()
        {
            return await _contexto.SaveChangesAsync();
        }
    }
}