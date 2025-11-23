namespace Requalify.Model;

public class Curso
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Area { get; set; } // texto simples: “Programação”, “Design”, etc.
}
