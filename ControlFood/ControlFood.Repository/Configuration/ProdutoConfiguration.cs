﻿using ControlFood.Repository.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlFood.Repository.Configuration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder
                .ToTable("Produto");

            builder
                .HasKey(x => x.Id)
                .HasName("Pk_produto_id");
            
            builder
                .Property(x => x.DataCadastro)
                .HasColumnType("date")
                .IsRequired();

            builder
                .Property(x => x.DataAlteracao)
                .HasColumnType("date");

            builder
                .Property(x => x.CodigoInterno)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(x => x.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .Property(x => x.ValorVenda)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder
                .Property(x => x.ValorCompra)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder
                .Property(x => x.DataValidade)
                .HasColumnType("date");

            builder
                .HasOne(x => x.Categoria)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.CategoriaId)
                .HasConstraintName("Produto_Categoria")
                .OnDelete(DeleteBehavior.Restrict);
           
        }
    }
}
