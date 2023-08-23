using ChallengeN5.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeN5.Repositories.Contexts;

public partial class N5DbContext : DbContext
{
    public N5DbContext()
    {
    }

    public N5DbContext(DbContextOptions<N5DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionType> PermissionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-6MQF69Q\\SQLEXPRESS;Initial Catalog=N5Challenge; User Id=eelicegui;Password=123456;TrustServerCertificate=True;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC07C8D7B783");

            entity.Property(e => e.Id).HasComment("Unique ID");
            entity.Property(e => e.ApellidoEmpleado)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasComment("Employee Surname");
            entity.Property(e => e.FechaPermiso)
                .HasComment("Permission granted on Date")
                .HasColumnType("date");
            entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasComment("Employee Forename");
            entity.Property(e => e.TipoPermiso).HasComment("Permission Type");

            entity.HasOne(d => d.TipoPermisoNavigation).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.TipoPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permissions_PermissionType");
        });

        modelBuilder.Entity<PermissionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC07103F7918");

            entity.Property(e => e.Id).HasComment("Unique ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Permission description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
