using Microsoft.AspNetCore.Mvc;
using Requalify.Services;
using Requalify.Model;
using Requalify.DTO;

namespace Requalify.Controllers.v1
{
    [ApiController]
    [Route("api/v1/noticia")]
    public class NoticiaController : ControllerBase
    {
        private readonly ServicoNoticias _service;

        public NoticiaController(ServicoNoticias service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var lista = await _service.ObterPaginadoAsync(pageNumber, pageSize);
            int total = await _service.ContarAsync();

            string baseUrl = $"{Request.Scheme}://{Request.Host}/api/v1/noticia";

            return Ok(new
            {
                total,
                pageNumber,
                pageSize,
                data = lista.Select(n => _service.MontarComLinks(n, baseUrl))
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var noticia = await _service.ObterPorIdAsync(id);
            if (noticia == null) return NotFound();

            string baseUrl = $"{Request.Scheme}://{Request.Host}/api/v1/noticia";

            return Ok(_service.MontarComLinks(noticia, baseUrl));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Noticia noticia)
        {
            var criado = await _service.CriarAsync(noticia);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Noticia noticia)
        {
            bool atualizado = await _service.AtualizarAsync(id, noticia);
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
