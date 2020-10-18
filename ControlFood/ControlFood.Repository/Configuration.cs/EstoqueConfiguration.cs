using ControlFood.Repository.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlFood.Repository.Configuration.cs
{
    public class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder
                .ToTable("Estoque");

            builder
                .HasKey(x => x.Id)
                .HasName("Pk_estoque_id");

            builder
                .Property(x => x.Quantidade)
                .HasColumnType("integer")
                .HasDefaultValue(0);

            builder
                .Property(x => x.DataValidade)
                .HasColumnType("date")
                .IsRequired();

            builder
                .Property(x => x.DataEntrada)
                .HasColumnType("date");

            builder
                .Property(x => x.ValorCompraUnidade)
                .HasColumnType("decimal")
                .HasDefaultValue(0)
                .IsRequired();

            builder
                .Property(x => x.ValorCompraTotal)
                .HasColumnType("decimal");

            builder
                .HasOne(x => x.Produto)
                .WithMany(x => x.Estoques)
                .HasForeignKey(x => x.IdProduto)
                .HasConstraintName("Estoque_Produto");
        }
    }
}
