using ControlFood.Repository.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlFood.Repository.Configuration
{
    class ProdutoAdicionalConfiguration : IEntityTypeConfiguration<ProdutoAdicional>
    {
        public void Configure(EntityTypeBuilder<ProdutoAdicional> builder)
        {
            builder
                .ToTable("ProdutoAdicional")
                .HasKey(x => new { x.AdicionalId, x.ProdutoId });

            builder
                .HasOne(x => x.Adicional)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.AdicionalId)
                .HasConstraintName("Adicional_Produto");

            builder
                .HasOne(x => x.Produto)
                .WithMany(x => x.Adicionais)
                .HasForeignKey(x => x.ProdutoId)
                .HasConstraintName("Produto_Adicional");
        }
    }
}
