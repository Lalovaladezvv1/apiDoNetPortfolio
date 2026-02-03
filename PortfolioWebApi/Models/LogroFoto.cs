using System;
using System.Collections.Generic;

namespace PortfolioWebApi.Models;

public partial class LogroFoto
{
    public int Id { get; set; }

    public int LogroId { get; set; }

    public string UrlFoto { get; set; } = null!;

    public int Orden { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Logro Logro { get; set; } = null!;
}
