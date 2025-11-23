namespace Requalify.DTO
{
    public class VagaHateoasDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int UsuarioId { get; set; }

        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
