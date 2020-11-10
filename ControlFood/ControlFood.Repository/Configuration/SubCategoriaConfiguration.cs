using ControlFood.Repository.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlFood.Repository.Configuration
{
    class SubCategoriaConfiguration : IEntityTypeConfiguration<SubCategoria>
    {
        public void Configure(EntityTypeBuilder<SubCategoria> builder)
        {

            builder
                .ToTable("SubCategoria");

            builder
                .HasKey(x => x.Id)
                .HasName("Pk_subCategoria_id");

            builder
                .Property(x => x.Tipo)
                .HasColumnType("Varchar(200)")
                .IsRequired();

            builder
                .Property(x => x.IndicadorItemCozinha)
                .HasColumnType("boolean")
                .HasDefaultValue(false);

            builder
                .Property(x => x.IndicadorItemBar)
                .HasColumnType("boolean")
                .HasDefaultValue(false);
            
            builder
              .Property(x => x.DataAlteracao)
              .HasColumnType("date");

            builder
                .Property(x => x.DataCadastro)
                .HasColumnType("date")
                .IsRequired();

            builder
                .HasOne(x => x.Categoria)
                .WithMany(x => x.SubCategorias)
                .HasForeignKey(x => x.CategoriaId)
                .HasConstraintName("SubCategoria_Categoria")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
