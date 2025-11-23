using Microsoft.EntityFrameworkCore;
using Requalify.Data;
using Requalify.Model;
using Requalify.DTO;

namespace Requalify.Services
{
    public class ServicoNoticias
    {
        private readonly AppDbContext _context;

        public ServicoNoticias(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Noticia>> ObterPaginadoAsync(int pageNumber, int pageSize)
        {
            return await _context.Noticias
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Noticias.CountAsync();
        }

        public async Task<Noticia?> ObterPorIdAsync(int id)
        {
            return await _context.Noticias.FindAsync(id);
        }

        public async Task<Noticia> CriarAsync(Noticia noticia)
        {
            _context.Noticias.Add(noticia);
            await _context.SaveChangesAsync();
            return noticia;
        }

        public async Task<bool> AtualizarAsync(int id, Noticia noticiaAtualizada)
        {
            var existe = await _context.Noticias.AnyAsync(n => n.Id == id);
            if (!existe) return false;

            noticiaAtualizada.Id = id;
            _context.Entry(noticiaAtualizada).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null) return false;

            _context.Noticias.Remove(noticia);
            await _context.SaveChangesAsync();
            return true;
        }

        public NoticiaHateoasDto MontarComLinks(Noticia noticia, string urlBase)
        {
            return new NoticiaHateoasDto
            {
                Id = noticia.Id,
                Titulo = noticia.Titulo,
                Descricao = noticia.Descricao,
                UrlImagem = noticia.UrlImagem,
                Links = new List<LinkDto>
                {
                    new LinkDto("self", $"{urlBase}/{noticia.Id}", "GET"),
                    new LinkDto("update", $"{urlBase}/{noticia.Id}", "PUT"),
                    new LinkDto("delete", $"{urlBase}/{noticia.Id}", "DELETE")
                }
            };
        }
    }
}
