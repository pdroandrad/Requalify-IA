using Microsoft.EntityFrameworkCore;
using Requalify.Data;
using Requalify.Model;
using Requalify.Services;

namespace Requalify.Tests.Services
{
    public class ServicoNoticiasTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CriarAsync_DeveCriarNoticia()
        {
            var context = GetDbContext();
            var service = new ServicoNoticias(context);

            var noticia = new Noticia
            {
                Titulo = "Mercado de TI cresce",
                Descricao = "Notícia teste",
                UrlImagem = "https://img.com/teste.png"
            };

            var criada = await service.CriarAsync(noticia);

            Assert.NotEqual(0, criada.Id);
        }
    }
}
