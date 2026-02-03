using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class LogroCreateDto
{
    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(100, ErrorMessage = "El título no puede superar 100 caracteres")]
    public string Titulo { get; set; } = null!;

    [Required(ErrorMessage = "La descripción es obligatoria")]
    [StringLength(500, ErrorMessage = "La descripción no puede superar 500 caracteres")]
    public string Descripcion { get; set; } = null!;

    [Required(ErrorMessage = "El tipo de entidad es obligatorio")]
    [StringLength(50)]
    public string TipoEntidad { get; set; } = null!;

    [Required]
    public int EntidadId { get; set; }

    [Range(0, 1000, ErrorMessage = "El orden debe estar entre 0 y 1000")]
    public int Orden { get; set; }

    public ICollection<LogroFotoCreateDto> LogroFotos { get; set; } = new List<LogroFotoCreateDto>();
}
