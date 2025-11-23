using Microsoft.EntityFrameworkCore;
using Requalify.Data;
using Requalify.Model;
using Requalify.DTO;

namespace Requalify.Services
{
    public class ServicoCursos
    {
        private readonly AppDbContext _context;

        public ServicoCursos(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Curso>> ObterPaginadoAsync(int pageNumber, int pageSize)
        {
            return await _context.Cursos
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Cursos.CountAsync();
        }

        public async Task<Curso?> ObterPorIdAsync(int id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        public async Task<Curso> CriarAsync(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<bool> AtualizarAsync(int id, Curso curso)
        {
            var existe = await _context.Cursos.AnyAsync(c => c.Id == id);
            if (!existe) return false;

            curso.Id = id;
            _context.Entry(curso).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return false;

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return true;
        }

        public CursoHateoasDto MontarComLinks(Curso curso, string urlBase)
        {
            return new CursoHateoasDto
            {
                Id = curso.Id,
                Titulo = curso.Titulo,
                Descricao = curso.Descricao,
                Links = new List<LinkDto>
                {
                    new LinkDto("self", $"{urlBase}/{curso.Id}", "GET"),
                    new LinkDto("update", $"{urlBase}/{curso.Id}", "PUT"),
                    new LinkDto("delete", $"{urlBase}/{curso.Id}", "DELETE")
                }
            };
        }
    }
}
