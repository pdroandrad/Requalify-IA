using Microsoft.AspNetCore.Mvc;
using Requalify.Services;
using Requalify.Model;
using Requalify.DTO;

namespace Requalify.Controllers.v1
{
    [ApiController]
    [Route("api/v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly ServicoUsuarios _service;

        public UsuarioController(ServicoUsuarios service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioHateoasDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var lista = await _service.ObterPaginadoAsync(pageNumber, pageSize);
            int total = await _service.ContarAsync();

            string baseUrl = $"{Request.Scheme}://{Request.Host}/api/v1/usuario";

            return Ok(new
            {
                total,
                pageNumber,
                pageSize,
                data = lista.Select(u => _service.MontarComLinks(u, baseUrl))
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioHateoasDto>> GetById(int id)
        {
            var usuario = await _service.ObterPorIdAsync(id);
            if (usuario == null) return NotFound();

            string baseUrl = $"{Request.Scheme}://{Request.Host}/api/v1/usuario";

            return Ok(_service.MontarComLinks(usuario, baseUrl));
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario usuario)
        {
            var criado = await _service.CriarAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Usuario usuario)
        {
            bool atualizado = await _service.AtualizarAsync(id, usuario);
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
