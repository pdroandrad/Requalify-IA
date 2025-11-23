namespace Requalify.DTO
{
    public class CursoHateoasDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int AreaId { get; set; }

        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
