using Microsoft.EntityFrameworkCore;
using PruebaTi.Api.Dominio.Entidades;

namespace PruebaTi.Api.Infraestructura.Contexto
{
    public class AplicacionDbContext : DbContext
    {
        public AplicacionDbContext(DbContextOptions<AplicacionDbContext> opciones)
            : base(opciones)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Favorito> Favoritos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entidad =>
            {
                entidad.ToTable("Usuarios");
                entidad.HasKey(u => u.Id);
                entidad.Property(u => u.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Favorito>(entidad =>
            {
                entidad.ToTable("Favoritos");
                entidad.HasKey(f => f.Id);

                entidad.Property(f => f.IdExterno)
                    .IsRequired()
                    .HasMaxLength(100);

                entidad.Property(f => f.Titulo)
                    .IsRequired()
                    .HasMaxLength(300);

                entidad.Property(f => f.Autores)
                    .HasMaxLength(500);

                entidad.Property(f => f.UrlPortada)
                    .HasMaxLength(500);

                entidad.HasOne(f => f.Usuario)
                    .WithMany(u => u.Favoritos)
                    .HasForeignKey(f => f.UsuarioId);

                entidad.HasIndex(f => new { f.UsuarioId, f.IdExterno })
                    .IsUnique();
            });
        }
    }
}
