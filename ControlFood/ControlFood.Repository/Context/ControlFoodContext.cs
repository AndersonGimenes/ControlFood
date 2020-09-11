using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using Respositorio = ControlFood.Repository.Entidades;

namespace ControlFood.Repository.Context
{
    public class ControlFoodContext : DbContext
    {
        public ControlFoodContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Respositorio.Categoria> Categoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var categoria = modelBuilder.Entity<Respositorio.Categoria>();

            categoria
                .HasKey(x => x.Id)
                .HasName("Categoria_Id");

            categoria
                .Property(x => x.Tipo)
                .HasColumnType("varchar(200)")
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

    }
}
