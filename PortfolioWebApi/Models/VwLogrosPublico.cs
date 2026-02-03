using System;
using System.Collections.Generic;

namespace PortfolioWebApi.Models;

public partial class VwLogrosPublico
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string TipoEntidad { get; set; } = null!;

    public int EntidadId { get; set; }

    public int Orden { get; set; }
}
