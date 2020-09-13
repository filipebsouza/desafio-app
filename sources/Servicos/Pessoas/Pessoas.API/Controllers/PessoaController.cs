using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pessoas.Dominio.Dtos;
using Pessoas.Dominio.Servicos;
using Pessoas.Infra.Consultas;

namespace Pessoas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        /// <summary>
        /// Listagem das pessoas
        /// </summary>
        /// <returns>
        /// Retorna uma lista de todas as pessoas cadastradas sem filtros
        /// </returns>
        [HttpGet]
        public ActionResult Listar([FromServices] IListagemDePessoas consulta) => Ok(consulta.Listar());

        /// <summary>
        /// Salvar dados das pessoas
        /// </summary>
        /// <returns>
        /// Retorna a pessoa salva
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> Salvar([FromServices] IArmazenadorDePessoa servico, [FromBody] PessoaDto dto) => Ok(await servico.Armazenar(dto));        
    }
}