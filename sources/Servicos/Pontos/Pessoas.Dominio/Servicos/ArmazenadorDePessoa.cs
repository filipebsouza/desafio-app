using System.Threading.Tasks;
using Base.Dominio;
using Pessoas.Dominio.Contratos;
using Pessoas.Dominio.Dtos;
using Pessoas.Dominio.Entidades;
using Pessoas.Dominio.Repositorios;

namespace Pessoas.Dominio.Servicos
{
    public class ArmazenadorDePessoa : ServicoDeDominioBase, IArmazenadorDePessoa
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;

        public ArmazenadorDePessoa(INotificadorBase notificador, IPessoaRepositorio pessoaRepositorio) : base(notificador)
        {
            _pessoaRepositorio = pessoaRepositorio;
        }

        public async Task<PessoaDto> Armazenar(PessoaDto dto)
        {
            if (!ValidarDto(dto)) return null;

            var pessoa = new Pessoa(dto.Nome, dto.DataDeNascimento, (TipoDaPessoaEnum)dto.TipoDaPessoa);

            if (pessoa.Invalid)
            {
                Notificador.Notificar(pessoa.Notificacoes);
                return null;
            }

            await _pessoaRepositorio.Incluir(pessoa);
            await _pessoaRepositorio.Salvar();

            return new PessoaDto
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                DataDeNascimento = pessoa.DataDeNascimento
            };
        }

        private bool ValidarDto(PessoaDto dto)
        {
            if (dto == null || dto.TipoDaPessoa <= 0)
            {
                Notificador.Notificar("Dto", PessoaDicionarioDeMensagensDeValidacao.MensagemDtoInvalido);
                return false;
            }

            return true;
        }
    }
}