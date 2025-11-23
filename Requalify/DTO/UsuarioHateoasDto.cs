using Requalify.Model;

namespace Requalify.DTO
{
    public class UsuarioHateoasDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;

        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
