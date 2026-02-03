using System;
using System.Collections.Generic;

namespace PortfolioWebApi.Models;

public partial class VwEducacionPublica
{
    public int Id { get; set; }

    public string Escuela { get; set; } = null!;

    public string Grado { get; set; } = null!;

    public string NombreCarrera { get; set; } = null!;

    public string Estatus { get; set; } = null!;

    public string? CedulaProfesional { get; set; }
}
