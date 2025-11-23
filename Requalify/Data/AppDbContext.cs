using Microsoft.EntityFrameworkCore;
using Requalify.Model;

namespace Requalify.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Noticia> Noticias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("USUARIOS_NET");
            modelBuilder.Entity<Curso>().ToTable("CURSOS_NET");
            modelBuilder.Entity<Vaga>().ToTable("VAGAS_NET");
            modelBuilder.Entity<Noticia>().ToTable("NOTICIAS_NET");

            modelBuilder.Entity<Vaga>()
                .HasOne(v => v.Recrutador)
                .WithMany()
                .HasForeignKey(v => v.UsuarioId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
