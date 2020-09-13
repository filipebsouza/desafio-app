using System.Threading.Tasks;
using Base.Dominio;
using Rendas.Dominio.Contratos;
using Rendas.Dominio.Dtos;
using Rendas.Dominio.Entidades;
using Rendas.Dominio.Repositorios;

namespace Rendas.Dominio.Servicos
{
    public class ArmazenadorDeRendaPorPessoa : ServicoDeDominioBase, IArmazenadorDeRendaPorPessoa
    {
        private readonly IRendaPorPessoaRepositorio _rendaPorPessoaRepositorio;

        public ArmazenadorDeRendaPorPessoa(INotificadorBase notificador, IRendaPorPessoaRepositorio rendaPorPessoaRepositorio) : base(notificador)
        {
            _rendaPorPessoaRepositorio = rendaPorPessoaRepositorio;
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

            return new RendaPorPessoaDto
            {
                Id = rendaPorPessoa.Id,
                PessoaId = rendaPorPessoa.PessoaId,
                NomePessoa = rendaPorPessoa.NomePessoa,
                Valor = rendaPorPessoa.Valor
            };
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