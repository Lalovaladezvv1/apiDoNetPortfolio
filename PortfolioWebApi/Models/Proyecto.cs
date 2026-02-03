using System;
using System.Collections.Generic;

namespace PortfolioWebApi.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Lugar { get; set; }

    public string Descripcion { get; set; } = null!;

    public string? UrlRepositorio { get; set; }

    public string? UrlDemo { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<ProyectoFoto> ProyectoFotos { get; set; } = new List<ProyectoFoto>();
}
