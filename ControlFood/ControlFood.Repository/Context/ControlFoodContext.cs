using Microsoft.EntityFrameworkCore;
using Repositorio = ControlFood.Repository.Entidades;

namespace ControlFood.Repository.Context
{
    public class ControlFoodContext : DbContext
    {
        public ControlFoodContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Repositorio.Categoria> Categoria { get; set; }
        public virtual DbSet<Repositorio.SubCategoria> SubCategoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var categoria = modelBuilder.Entity<Repositorio.Categoria>();
            categoria
                .HasKey(x => x.Id)
                .HasName("Categoria_Id");

            categoria
                .Property(x => x.Tipo)
                .HasColumnType("varchar(200)")
                .IsRequired();

            var subCategoria = modelBuilder.Entity<Repositorio.SubCategoria>();
            subCategoria
                .HasKey(x => x.Id)
                .HasName("SubCategoria_Id");

            subCategoria
                .Property(x => x.Tipo)
                .HasColumnType("Varchar(200)")
                .IsRequired();

            subCategoria
                .HasOne(x => x.Categoria)
                .WithMany(x => x.SubCategorias)
                .HasForeignKey(x => x.CategoriaId)
                .HasConstraintName("SubCategoria_Categoria");

            base.OnModelCreating(modelBuilder);
        }

    }
}
