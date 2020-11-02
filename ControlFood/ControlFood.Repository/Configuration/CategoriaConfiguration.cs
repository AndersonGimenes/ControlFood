using ControlFood.Repository.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlFood.Repository.Configuration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {

        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder
               .ToTable("Categoria");

            builder
               .HasKey(x => x.Id)
               .HasName("Pk_categoria_id");

            builder
               .Property(x => x.Tipo)
               .HasColumnType("varchar(200)")
               .IsRequired();

        }
    }
}
