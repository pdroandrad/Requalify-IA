using Microsoft.AspNetCore.Mvc;
using Requalify.Services;
using Requalify.Model;
using Requalify.DTO;

namespace Requalify.Controllers.v1
{
    [ApiController]
    [Route("api/v1/vaga")]
    public class VagaController : ControllerBase
    {
        private readonly ServicoVagas _service;

        public VagaController(ServicoVagas service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var lista = await _service.ObterPaginadoAsync(pageNumber, pageSize);
            int total = await _service.ContarAsync();

            string baseUrl = $"{Request.Scheme}://{Request.Host}/api/v1/vaga";

            return Ok(new
            {
                total,
                pageNumber,
                pageSize,
                data = lista.Select(v => _service.MontarComLinks(v, baseUrl))
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vaga = await _service.ObterPorIdAsync(id);
            if (vaga == null) return NotFound();

            string baseUrl = $"{Request.Scheme}://{Request.Host}/api/v1/vaga";

            return Ok(_service.MontarComLinks(vaga, baseUrl));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vaga vaga)
        {
            var criado = await _service.CriarAsync(vaga);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Vaga vaga)
        {
            bool atualizado = await _service.AtualizarAsync(id, vaga);
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
