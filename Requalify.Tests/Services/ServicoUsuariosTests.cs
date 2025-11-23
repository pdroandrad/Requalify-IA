using Microsoft.EntityFrameworkCore;
using Requalify.Data;
using Requalify.Model;
using Requalify.Services;

namespace Requalify.Tests.Services
{
    public class ServicoUsuariosTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CriarAsync_DeveCriarUsuario()
        {
            var context = GetDbContext();
            var service = new ServicoUsuarios(context);

            var usuario = new Usuario
            {
                Nome = "Pedro",
                Sobrenome = "Andrade",
                Email = "pedro@test.com",
                Senha = "123",
                Tipo = "candidato"
            };

            var criado = await service.CriarAsync(usuario);

            Assert.NotEqual(0, criado.Id);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarNull_SeNaoExistir()
        {
            var context = GetDbContext();
            var service = new ServicoUsuarios(context);

            var usuario = await service.ObterPorIdAsync(999);

            Assert.Null(usuario);
        }
    }
}
