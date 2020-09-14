using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Dominio;
using Pontos.Dominio.Contratos;
using Pontos.Dominio.Dtos;
using Pontos.Dominio.Entidades;
using Pontos.Dominio.Repositorios;

namespace Pontos.Dominio.Servicos
{
    public class ArmazenadorDePontuacaoPorFamilia : ServicoDeDominioBase, IArmazenadorDePontuacaoPorFamilia
    {
        private readonly PessoasContrato _pessoasContrato;
        private readonly RendaPorPessoasContrato _rendaPorPessoasContrato;
        private readonly IPontuacaoPorFamiliaRepositorio _pontuacaoPorFamiliaRepositorio;

        public ArmazenadorDePontuacaoPorFamilia(
            INotificadorBase notificador,
            PessoasContrato pessoasContrato,
            RendaPorPessoasContrato rendaPorPessoasContrato,
            IPontuacaoPorFamiliaRepositorio pontuacaoPorFamiliaRepositorio
        ) : base(notificador)
        {
            _pessoasContrato = pessoasContrato;
            _rendaPorPessoasContrato = rendaPorPessoasContrato;
            _pontuacaoPorFamiliaRepositorio = pontuacaoPorFamiliaRepositorio;
        }

        public async Task<FamiliaDto> Armazenar(FamiliaDto dto)
        {
            if (!ValidarDto(dto)) return null;

            if (!ValidarPessoas(dto.Pessoas)) return null;

            if (!ValidarRendaPorPessoas(dto.RendaPorPessoas)) return null;

            var familia = new Familia(
                dto.Id,
                InstanciaPessoas(dto.Pessoas),
                InstanciaRendaPorPessoas(dto.RendaPorPessoas),
                dto.Status
            );

            var pontuacaoPorFamilia = new PontuacaoPorFamilia(familia);

            if (pontuacaoPorFamilia.Invalid)
            {
                Notificador.Notificar(pontuacaoPorFamilia.Notificacoes);
                return null;
            }

            await _pontuacaoPorFamiliaRepositorio.Incluir(pontuacaoPorFamilia);
            await _pontuacaoPorFamiliaRepositorio.Salvar();

            return MontarDtoDeRetorno(pontuacaoPorFamilia);
        }

        private FamiliaDto MontarDtoDeRetorno(PontuacaoPorFamilia pontuacaoPorFamilia)
        {
            return new FamiliaDto
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
            };
        }

        private List<RendaPorPessoa> InstanciaRendaPorPessoas(List<RendaPorPessoaDto> dto)
        {
            var rendaPorPessoas = new List<RendaPorPessoa>();

            foreach (var item in dto)
            {
                rendaPorPessoas.Add(new RendaPorPessoa(item.Id, item.PessoaId, item.Valor));
            }

            return rendaPorPessoas;
        }

        private List<Pessoa> InstanciaPessoas(List<PessoaDto> dto)
        {
            var pessoas = new List<Pessoa>();

            foreach (var item in dto)
            {
                pessoas.Add(new Pessoa(item.Id, item.Nome, item.DataDeNascimento, (TipoDaPessoaEnum)item.TipoDaPessoa));
            }

            return pessoas;
        }

        private bool ValidarRendaPorPessoas(List<RendaPorPessoaDto> rendaPorPessoas)
        {
            _rendaPorPessoasContrato.ValidarContrato(rendaPorPessoas);

            if (_rendaPorPessoasContrato.Invalid)
                Notificador.Notificar(_pessoasContrato.Notifications);

            return _rendaPorPessoasContrato.Valid;
        }

        private bool ValidarPessoas(List<PessoaDto> pessoas)
        {
            _pessoasContrato.ValidarContrato(pessoas);

            if (_pessoasContrato.Invalid)
                Notificador.Notificar(_pessoasContrato.Notifications);

            return _pessoasContrato.Valid;
        }

        private bool ValidarDto(FamiliaDto dto)
        {
            if (dto == null)
            {
                Notificador.Notificar("Dto", PontuacaoPorFamiliaDicionarioDeMensagensDeValidacao.MensagemDtoInvalido);
                return false;
            }

            return true;
        }
    }
}