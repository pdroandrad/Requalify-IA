using Microsoft.EntityFrameworkCore;
using Requalify.Data;
using Requalify.Model;
using Requalify.DTO;

namespace Requalify.Services
{
    public class ServicoUsuarios
    {
        private readonly AppDbContext _context;

        public ServicoUsuarios(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObterPaginadoAsync(int pageNumber, int pageSize)
        {
            return await _context.Usuarios
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Usuarios.CountAsync();
        }

        public async Task<Usuario?> ObterPorIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> CriarAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> AtualizarAsync(int id, Usuario usuario)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Id == id);
            if (!existe) return false;

            usuario.Id = id;
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public UsuarioHateoasDto MontarComLinks(Usuario usuario, string urlBase)
        {
            return new UsuarioHateoasDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                Tipo = usuario.Tipo,
                Links = new List<LinkDto>
                {
                    new LinkDto("self", $"{urlBase}/{usuario.Id}", "GET"),
                    new LinkDto("update", $"{urlBase}/{usuario.Id}", "PUT"),
                    new LinkDto("delete", $"{urlBase}/{usuario.Id}", "DELETE")
                }
            };
        }
    }
}
