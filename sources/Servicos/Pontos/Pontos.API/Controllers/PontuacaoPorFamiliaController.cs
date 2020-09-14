using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pontos.Dominio.Dtos;
using Pontos.Dominio.Servicos;
using Pontos.Infra.Consultas;

namespace Pontos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PontuacaoPorFamiliaController : ControllerBase
    {
        /// <summary>
        /// Listagem das Familias ordenados pela pontuacao
        /// </summary>
        /// <returns>
        /// Retorna uma lista de todas as Familias ordenadas por pontuacao sem filtros
        /// </returns>
        [HttpGet]
        public ActionResult Listar([FromServices] IListagemDeFamiliasPorPontuacao consulta) => Ok(consulta.Listar());

        /// <summary>
        /// Salvar dados da Pontuacao por familia
        /// </summary>
        /// <returns>
        /// Retorna a familia salva
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> Salvar([FromServices] IArmazenadorDePontuacaoPorFamilia servico, [FromBody] FamiliaDto dto) => 
            Ok(await servico.Armazenar(dto));        
    }
}