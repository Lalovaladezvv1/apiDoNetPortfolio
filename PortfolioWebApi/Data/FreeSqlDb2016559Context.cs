using Microsoft.EntityFrameworkCore;

namespace PortfolioWebApi.Models;

public partial class FreeSqlDb2016559Context : DbContext
{
    public FreeSqlDb2016559Context()
    {
    }

    public FreeSqlDb2016559Context(DbContextOptions<FreeSqlDb2016559Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Educacion> Educacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<ExperienciaLaboral> ExperienciaLaborals { get; set; }

    public virtual DbSet<Logro> Logros { get; set; }

    public virtual DbSet<LogroFoto> LogroFotos { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<ProyectoFoto> ProyectoFotos { get; set; }

    public virtual DbSet<VwEducacionPublica> VwEducacionPublicas { get; set; }

    public virtual DbSet<VwExperienciaLaboralPublica> VwExperienciaLaboralPublicas { get; set; }

    public virtual DbSet<VwLogrosPublico> VwLogrosPublicos { get; set; }

    public virtual DbSet<VwProyectosPublico> VwProyectosPublicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Educacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Educacio__3214EC0741C3EE7A");

            entity.ToTable("Educacion");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CedulaProfesional).HasMaxLength(50);
            entity.Property(e => e.Escuela).HasMaxLength(150);
            entity.Property(e => e.Estatus).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Grado).HasMaxLength(100);
            entity.Property(e => e.NombreCarrera).HasMaxLength(150);
        });

        modelBuilder.Entity<ExperienciaLaboral>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Experien__3214EC07FD3DB6DC");

            entity.ToTable("ExperienciaLaboral");

            entity.HasIndex(e => e.Activo, "IX_ExperienciaLaboral_Activo");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Empresa).HasMaxLength(150);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Lugar).HasMaxLength(100);
            entity.Property(e => e.Modalidad).HasMaxLength(50);
            entity.Property(e => e.Puesto).HasMaxLength(100);
            entity.Property(e => e.UrlLogoEmpresa).HasMaxLength(500);
        });

        modelBuilder.Entity<Logro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Logro__3214EC076E1EF764");

            entity.ToTable("Logro");

            entity.HasIndex(e => new { e.TipoEntidad, e.EntidadId, e.Activo }, "IX_Logro_Entidad");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Orden).HasDefaultValue(1);
            entity.Property(e => e.TipoEntidad).HasMaxLength(50);
            entity.Property(e => e.Titulo).HasMaxLength(150);
        });

        modelBuilder.Entity<LogroFoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LogroFot__3214EC07F7C58EF3");

            entity.ToTable("LogroFoto");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Orden).HasDefaultValue(1);
            entity.Property(e => e.UrlFoto).HasMaxLength(500);

            entity.HasOne(d => d.Logro).WithMany(p => p.LogroFotos)
                .HasForeignKey(d => d.LogroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogroFoto_Logro");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3214EC07171A92D4");

            entity.ToTable("Proyecto");

            entity.HasIndex(e => e.Activo, "IX_Proyecto_Activo");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Lugar).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.UrlDemo).HasMaxLength(500);
            entity.Property(e => e.UrlRepositorio).HasMaxLength(500);
        });

        modelBuilder.Entity<ProyectoFoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3214EC07EABB4CDE");

            entity.ToTable("ProyectoFoto");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Orden).HasDefaultValue(1);
            entity.Property(e => e.UrlFoto).HasMaxLength(500);

            entity.HasOne(d => d.Proyecto).WithMany(p => p.ProyectoFotos)
                .HasForeignKey(d => d.ProyectoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProyectoFoto_Proyecto");
        });

        modelBuilder.Entity<VwEducacionPublica>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Educacion_Publica");

            entity.Property(e => e.CedulaProfesional).HasMaxLength(50);
            entity.Property(e => e.Escuela).HasMaxLength(150);
            entity.Property(e => e.Estatus).HasMaxLength(50);
            entity.Property(e => e.Grado).HasMaxLength(100);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.NombreCarrera).HasMaxLength(150);
        });

        modelBuilder.Entity<VwExperienciaLaboralPublica>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ExperienciaLaboral_Publica");

            entity.Property(e => e.Empresa).HasMaxLength(150);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Lugar).HasMaxLength(100);
            entity.Property(e => e.Modalidad).HasMaxLength(50);
            entity.Property(e => e.Puesto).HasMaxLength(100);
            entity.Property(e => e.UrlLogoEmpresa).HasMaxLength(500);
        });

        modelBuilder.Entity<VwLogrosPublico>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Logros_Publicos");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.TipoEntidad).HasMaxLength(50);
            entity.Property(e => e.Titulo).HasMaxLength(150);
        });

        modelBuilder.Entity<VwProyectosPublico>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Proyectos_Publicos");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Lugar).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.UrlDemo).HasMaxLength(500);
            entity.Property(e => e.UrlRepositorio).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
