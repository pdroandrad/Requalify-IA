using Microsoft.EntityFrameworkCore;
using Requalify.Data;
using Requalify.Model;
using Requalify.DTO;

namespace Requalify.Services
{
    public class ServicoVagas
    {
        private readonly AppDbContext _context;

        public ServicoVagas(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vaga>> ObterPaginadoAsync(int pageNumber, int pageSize)
        {
            return await _context.Vagas
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Vagas.CountAsync();
        }

        public async Task<Vaga?> ObterPorIdAsync(int id)
        {
            return await _context.Vagas.FindAsync(id);
        }

        public async Task<Vaga> CriarAsync(Vaga vaga)
        {
            _context.Vagas.Add(vaga);
            await _context.SaveChangesAsync();
            return vaga;
        }

        public async Task<bool> AtualizarAsync(int id, Vaga vagaAtualizada)
        {
            var existe = await _context.Vagas.AnyAsync(v => v.Id == id);
            if (!existe) return false;

            vagaAtualizada.Id = id;
            _context.Entry(vagaAtualizada).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga == null) return false;

            _context.Vagas.Remove(vaga);
            await _context.SaveChangesAsync();
            return true;
        }

        public VagaHateoasDto MontarComLinks(Vaga vaga, string urlBase)
        {
            return new VagaHateoasDto
            {
                Id = vaga.Id,
                Titulo = vaga.Titulo,
                Descricao = vaga.Descricao,
                UsuarioId = vaga.UsuarioId,
                Links = new List<LinkDto>
                {
                    new LinkDto("self", $"{urlBase}/{vaga.Id}", "GET"),
                    new LinkDto("update", $"{urlBase}/{vaga.Id}", "PUT"),
                    new LinkDto("delete", $"{urlBase}/{vaga.Id}", "DELETE")
                }
            };
        }
    }
}
