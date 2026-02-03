using PortfolioWebApi.Models;
public class LogroReadDto
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string TipoEntidad { get; set; } = null!;

    public int EntidadId { get; set; }

    public int Orden { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<LogroFoto> LogroFotos { get; set; } = new List<LogroFoto>();
}