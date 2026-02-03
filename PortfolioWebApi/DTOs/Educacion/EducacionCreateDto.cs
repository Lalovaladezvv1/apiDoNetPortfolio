using System.ComponentModel.DataAnnotations;

public class EducacionCreateDto
{
    [Required]
    [MaxLength(150)]
    public string Escuela { get; set; }

    [Required]
    [MaxLength(50)]
    public string Grado { get; set; }

    [Required]
    [MaxLength(100)]
    public string NombreCarrera { get; set; }

    [MaxLength(50)]
    public string CedulaProfesional { get; set; }

    [Required]
    public string Estatus { get; set; }
}
