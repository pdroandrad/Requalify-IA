using Microsoft.AspNetCore.Mvc;
using Requalify.Services;
using Requalify.Model;
using Requalify.DTO;

namespace Requalify.Controllers.v1
{
    [ApiController]
    [Route("api/v1/curso")]
    public class CursoController : ControllerBase
    {
        private readonly ServicoCursos _service;

        public CursoController(ServicoCursos service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var lista = await _service.ObterPaginadoAsync(pageNumber, pageSize);
            int total = await _service.ContarAsync();

            string baseUrl = $"{Request.Scheme}://{Request.Host}/api/v1/curso";

            return Ok(new
            {
                total,
                pageNumber,
                pageSize,
                data = lista.Select(c => _service.MontarComLinks(c, baseUrl))
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var curso = await _service.ObterPorIdAsync(id);
            if (curso == null) return NotFound();

            string baseUrl = $"{Request.Scheme}://{Request.Host}/api/v1/curso";

            return Ok(_service.MontarComLinks(curso, baseUrl));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Curso curso)
        {
            var criado = await _service.CriarAsync(curso);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Curso curso)
        {
            bool atualizado = await _service.AtualizarAsync(id, curso);
            if (!atualizado) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool removido = await _service.RemoverAsync(id);
            if (!removido) return NotFound();

            return NoContent();
        }
    }
}
