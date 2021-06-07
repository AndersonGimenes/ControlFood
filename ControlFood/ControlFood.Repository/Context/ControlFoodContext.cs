using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Repositorio = ControlFood.Repository.Entidades;

namespace ControlFood.Repository.Context
{
    public class ControlFoodContext : DbContext
    {
        public ControlFoodContext(DbContextOptions<ControlFoodContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Repositorio.Categoria> Categoria { get; set; }
        public virtual DbSet<Models.ProdutoVenda> Produto { get; set; }
        public virtual DbSet<Repositorio.Adicional> Adicional { get; set; }
        public virtual DbSet<Repositorio.ProdutoAdicional> ProdutoAdicional { get; set; }
        public virtual DbSet<Repositorio.Cliente> Cliente { get; set; }
        public virtual DbSet<Repositorio.Endereco> Endereco { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
        }

    }
}
