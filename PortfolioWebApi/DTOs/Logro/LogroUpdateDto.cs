using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class LogroUpdateDto
{
    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(100)]
    public string Titulo { get; set; } = null!;

    [Required]
    [StringLength(500)]
    public string Descripcion { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string TipoEntidad { get; set; } = null!;

    [Required]
    public int EntidadId { get; set; }

    [Range(0, 1000)]
    public int Orden { get; set; }

    public bool Activo { get; set; }

    public ICollection<LogroFotoUpdateDto> LogroFotos { get; set; } = new List<LogroFotoUpdateDto>();
}
