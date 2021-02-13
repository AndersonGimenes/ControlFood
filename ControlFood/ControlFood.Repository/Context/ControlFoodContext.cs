using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
        public virtual DbSet<Repositorio.Produto> Produto { get; set; }
        //public virtual DbSet<Repositorio.Cliente> Cliente { get; set; }
        //public virtual DbSet<Repositorio.Endereco> Endereco { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
