using System.Threading.Tasks;
using Pontos.Dominio.Entidades;
using Pontos.Dominio.Repositorios;
using Pontos.Infra.Contextos;

namespace Pontos.Infra.Repositorios
{
    public class PontuacaoPorFamiliaRepositorio : IPontuacaoPorFamiliaRepositorio
    {
        private readonly PontoContexto _contexto;

        public PontuacaoPorFamiliaRepositorio(PontoContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<PontuacaoPorFamilia> Incluir(PontuacaoPorFamilia pontuacaoPorFamilia)
        {
            await _contexto.Set<PontuacaoPorFamilia>().AddAsync(pontuacaoPorFamilia);

            return pontuacaoPorFamilia;
        }

        public async Task<int> Salvar()
        {
            return await _contexto.SaveChangesAsync();
        }
    }
}