using System;
using System.Collections.Generic;

namespace PortfolioWebApi.Models;

public partial class ExperienciaLaboral
{
    public int Id { get; set; }

    public string Puesto { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string FechaInicio { get; set; }

    public string FechaFin { get; set; }

    public string Lugar { get; set; } = null!;

    public string Modalidad { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? UrlLogoEmpresa { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }
}
