using Microsoft.EntityFrameworkCore;
using Requalify.Data;
using Requalify.Model;
using Requalify.Services;

namespace Requalify.Tests.Services
{
    public class ServicoVagasTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CriarAsync_DeveCriarVaga()
        {
            var context = GetDbContext();
            var service = new ServicoVagas(context);

            var usuario = new Usuario
            {
                Nome = "Recrutador",
                Sobrenome = "Teste",
                Email = "r@test.com",
                Senha = "123",
                Tipo = "recrutador"
            };

            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var vaga = new Vaga
            {
                UsuarioId = usuario.Id,
                Titulo = "Dev Jr",
                Descricao = "Vaga de teste"
            };

            var criada = await service.CriarAsync(vaga);

            Assert.NotEqual(0, criada.Id);
        }
    }
}
