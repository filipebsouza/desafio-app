using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Dominio;
using Familias.Dominio.Contratos;
using Familias.Dominio.Dtos;
using Familias.Dominio.Entidades;
using Familias.Dominio.Repositorios;

namespace Familias.Dominio.Servicos
{
    public class ArmazenadorDeFamilia : ServicoDeDominioBase, IArmazenadorDeFamilia
    {
        private readonly PessoasContrato _pessoasContrato;
        private readonly RendaPorPessoasContrato _rendaPorPessoasContrato;
        private readonly IValidadorDePessoaJahInformadaEmOutraFamilia _validadorDePessoaJahInformadaEmOutraFamilia;
        private readonly IFamiliaRepositorio _familiaRepositorio;

        public ArmazenadorDeFamilia(
            INotificadorBase notificador,
            PessoasContrato pessoasContrato,
            RendaPorPessoasContrato rendaPorPessoasContrato,
            IValidadorDePessoaJahInformadaEmOutraFamilia validadorDePessoaJahInformadaEmOutraFamilia,
            IFamiliaRepositorio familiaRepositorio
        ) : base(notificador)
        {
            _pessoasContrato = pessoasContrato;
            _rendaPorPessoasContrato = rendaPorPessoasContrato;
            _validadorDePessoaJahInformadaEmOutraFamilia = validadorDePessoaJahInformadaEmOutraFamilia;
            _familiaRepositorio = familiaRepositorio;
        }

        public async Task<FamiliaDto> Armazenar(FamiliaDto dto)
        {
            if (!ValidarDto(dto)) return null;

            if (!ValidarPessoas(dto.Pessoas)) return null;

            if (!ValidarRendaPorPessoas(dto.RendaPorPessoas)) return null;

            if (await ValidarSeHaPessoasJahInformadasEmOutraFamilia(dto.Pessoas)) return null;

            var familia = new Familia(
                InstanciaPessoas(dto.Pessoas),
                InstanciaRendaPorPessoas(dto.RendaPorPessoas)
            );

            if (familia.Invalid)
            {
                Notificador.Notificar(familia.Notificacoes);
                return null;
            }

            await _familiaRepositorio.Incluir(familia);
            await _familiaRepositorio.Salvar();

            return MontarDtoDeRetorno(familia);
        }

        private async Task<bool> ValidarSeHaPessoasJahInformadasEmOutraFamilia(List<PessoaDto> pessoas)
        {
            var pessoasIds = pessoas.Select(p => p.Id).ToList();

            var possui = await _validadorDePessoaJahInformadaEmOutraFamilia.PessoaJahFoiInformada(pessoasIds);

            return possui;
        }

        private FamiliaDto MontarDtoDeRetorno(Familia familia)
        {
            return new FamiliaDto
            {
                Id = familia.Id,
                Pessoas = familia.Pessoas.Select(pessoa => new PessoaDto
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    DataDeNascimento = pessoa.DataDeNascimento,
                    DescricaoTipoDaPessoa = pessoa.DescricaoTipoDaPessoa
                })
                .ToList(),
                RendaPorPessoas = familia.Rendas.Select(rendaPorPessoa => new RendaPorPessoaDto
                {
                    Id = rendaPorPessoa.Id,
                    PessoaId = rendaPorPessoa.PessoaId,
                    Valor = rendaPorPessoa.Valor
                })
                .ToList(),
                Status = (int)familia.Status
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
                pessoas.Add(new Pessoa(item.Id, item.Nome, item.DataDeNascimento, item.DescricaoTipoDaPessoa));
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
                Notificador.Notificar("Dto", FamiliaDicionarioDeMensagensDeValidacao.MensagemDtoInvalido);
                return false;
            }

            return true;
        }
    }
}