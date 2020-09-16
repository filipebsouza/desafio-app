using System.Threading.Tasks;
using Base.Dominio.Notificacoes;
using Rendas.Dominio.Contratos;
using Rendas.Dominio.Dtos;
using Rendas.Dominio.Entidades;
using Rendas.Dominio.Repositorios;

namespace Rendas.Dominio.Servicos
{
    public class ArmazenadorDeRendaPorPessoa : ServicoDeDominioBase, IArmazenadorDeRendaPorPessoa
    {
        private readonly IRendaPorPessoaRepositorio _rendaPorPessoaRepositorio;
        private readonly IAlteradorDePontosPorInsercaoDeRenda _alteradorDePontosPorInsercaoDeRenda;

        public ArmazenadorDeRendaPorPessoa(
            INotificadorBase notificador,
            IRendaPorPessoaRepositorio rendaPorPessoaRepositorio,
            IAlteradorDePontosPorInsercaoDeRenda alteradorDePontosPorInsercaoDeRenda
        ) : base(notificador)
        {
            _rendaPorPessoaRepositorio = rendaPorPessoaRepositorio;
            _alteradorDePontosPorInsercaoDeRenda = alteradorDePontosPorInsercaoDeRenda;
        }

        public async Task<RendaPorPessoaDto> Armazenar(RendaPorPessoaDto dto)
        {
            if (!ValidarDto(dto)) return null;

            var rendaPorPessoa = new RendaPorPessoa(
                dto.PessoaId,
                dto.NomePessoa,
                dto.Valor);

            if (rendaPorPessoa.Invalid)
            {
                Notificador.Notificar(rendaPorPessoa.Notificacoes);
                return null;
            }

            await _rendaPorPessoaRepositorio.Incluir(rendaPorPessoa);
            await _rendaPorPessoaRepositorio.Salvar();

            var retorno = new RendaPorPessoaDto
            {
                Id = rendaPorPessoa.Id,
                PessoaId = rendaPorPessoa.PessoaId,
                NomePessoa = rendaPorPessoa.NomePessoa,
                Valor = rendaPorPessoa.Valor
            };

            _alteradorDePontosPorInsercaoDeRenda.Alterar(retorno);

            return retorno;
        }

        private bool ValidarDto(RendaPorPessoaDto dto)
        {
            if (dto == null)
            {
                Notificador.Notificar("Dto", RendaPorPessoaDicionarioDeMensagensDeValidacao.MensagemDtoInvalido);
                return false;
            }

            return true;
        }
    }
}