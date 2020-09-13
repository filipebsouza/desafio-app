using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rendas.Dominio.Dtos;
using Rendas.Dominio.Servicos;
using Rendas.Infra.Consultas;

namespace Rendas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RendaPorPessoaController : ControllerBase
    {
        /// <summary>
        /// Listagem das rendas por pessoa
        /// </summary>
        /// <returns>
        /// Retorna uma lista de todas as rendas por pessoas cadastradas sem filtros
        /// </returns>
        [HttpGet]
        public ActionResult Listar([FromServices] IListagemDeRendaPorPessoas consulta) => Ok(consulta.Listar());

        /// <summary>
        /// Salvar dados das rendas por pessoa
        /// </summary>
        /// <returns>
        /// Retorna a renda salva
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> Salvar([FromServices] IArmazenadorDeRendaPorPessoa servico, [FromBody] RendaPorPessoaDto dto) =>
            Ok(await servico.Armazenar(dto));
    }
}