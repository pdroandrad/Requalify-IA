using Microsoft.AspNetCore.Mvc;
using Requalify.Services;

namespace Requalify.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class IAController : ControllerBase
    {
        private readonly AiRecommendationService _ai;

        public IAController(AiRecommendationService ai)
        {
            _ai = ai;
        }

        [HttpPost("recomendacoes")]
        public async Task<IActionResult> GerarRecomendacoes([FromBody] RequisicaoIA dto)
        {
            var resultado = await _ai.GerarSugestaoCursos(dto.AreaDeInteresse, dto.NivelExperiencia);

            return Ok(new
            {
                recomendacao = resultado
            });
        }
    }

    // DTO
    public class RequisicaoIA
    {
        public string AreaDeInteresse { get; set; }
        public string NivelExperiencia { get; set; }
    }
}
