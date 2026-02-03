using System.ComponentModel.DataAnnotations;


public class ExperienciaLaboralCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Puesto { get; set; }

    [Required]
    [MaxLength(100)]
    public string Empresa { get; set; }

    [Required]
    public string FechaInicio { get; set; }

    public string FechaFin { get; set; }

    [MaxLength(100)]
    public string Lugar { get; set; }

    [MaxLength(50)]
    public string Modalidad { get; set; }

    [MaxLength(500)]
    public string Descripcion { get; set; }

    [MaxLength(200)]
    public string UrlLogoEmpresa { get; set; }
}