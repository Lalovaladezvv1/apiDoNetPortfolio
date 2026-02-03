using System;
using System.Collections.Generic;

namespace PortfolioWebApi.Models;

public partial class VwProyectosPublico
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Lugar { get; set; }

    public string Descripcion { get; set; } = null!;

    public string? UrlRepositorio { get; set; }

    public string? UrlDemo { get; set; }
}
