using System.ComponentModel.DataAnnotations;

public class LogroFotoUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "La URL de la foto es obligatoria")]
    [Url(ErrorMessage = "Debe ser una URL válida")]
    public string UrlFoto { get; set; } = null!;

    [Required]
    [Range(0, 1000, ErrorMessage = "El orden debe estar entre 0 y 1000")]
    public int Orden { get; set; }

    public bool Activo { get; set; }
}
