using System.Threading.Tasks;
using Rendas.Dominio.Entidades;
using Rendas.Dominio.Repositorios;
using Rendas.Infra.Contextos;

namespace Rendas.Infra.Repositorios
{
    public class RendaPorPessoaRepositorio : IRendaPorPessoaRepositorio
    {
        private readonly RendaPorPessoaContexto _contexto;

        public RendaPorPessoaRepositorio(RendaPorPessoaContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<RendaPorPessoa> Incluir(RendaPorPessoa rendaPorPessoa)
        {
            await _contexto.Set<RendaPorPessoa>().AddAsync(rendaPorPessoa);

            return rendaPorPessoa;
        }

        public async Task<int> Salvar()
        {
            return await _contexto.SaveChangesAsync();
        }
    }
}