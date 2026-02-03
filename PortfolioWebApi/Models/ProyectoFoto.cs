using System;
using System.Collections.Generic;

namespace PortfolioWebApi.Models;

public partial class ProyectoFoto
{
    public int Id { get; set; }

    public int ProyectoId { get; set; }

    public string UrlFoto { get; set; } = null!;

    public int Orden { get; set; }

    public bool Activo { get; set; }

    public virtual Proyecto Proyecto { get; set; } = null!;
}
