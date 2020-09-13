using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Familias.Dominio.Dtos;
using Familias.Dominio.Servicos;
using Familias.Infra.Consultas;

namespace Familias.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamiliaController : ControllerBase
    {
        /// <summary>
        /// Listagem das familias
        /// </summary>
        /// <returns>
        /// Retorna uma lista de todas as familias sem filtros
        /// </returns>
        [HttpGet]
        public ActionResult Listar([FromServices] IListagemDeFamilias consulta) => Ok(consulta.Listar());

        /// <summary>
        /// Salvar dados das familias
        /// </summary>
        /// <returns>
        /// Retorna a familia salva
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> Salvar([FromServices] IArmazenadorDeFamilia servico, [FromBody] FamiliaDto dto) =>
            Ok(await servico.Armazenar(dto));
    }
}