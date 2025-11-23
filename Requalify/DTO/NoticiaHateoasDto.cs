namespace Requalify.DTO
{
    public class NoticiaHateoasDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string UrlImagem { get; set; } = string.Empty;

        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
