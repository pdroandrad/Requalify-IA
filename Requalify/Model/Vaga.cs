using Requalify.Model;
using System.ComponentModel.DataAnnotations;

public class Vaga
{
    public int Id { get; set; }

    [Required]
    public int UsuarioId { get; set; }   // FK

    // não obrigatória no POST
    public Usuario? Recrutador { get; set; }

    public string Titulo { get; set; }
    public string Descricao { get; set; }
}
