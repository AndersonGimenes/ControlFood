using ControlFood.Repository.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ControlFood.Repository.Configuration
{
    class AdicionalConfiguration : IEntityTypeConfiguration<Adicional>
    {
        public void Configure(EntityTypeBuilder<Adicional> builder)
        {
            builder
                .ToTable("Adicional");

            builder
                .HasKey(x => x.Id)
                .HasName("Pk_adicional_id");

            builder
                .Property(x => x.Tipo)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .Property(x => x.DataCadastro)
                .HasColumnType("date")
                .IsRequired();

            builder
                .Property(x => x.DataAlteracao)
                .HasColumnType("date");

            builder
                .Property(x => x.Valor)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
