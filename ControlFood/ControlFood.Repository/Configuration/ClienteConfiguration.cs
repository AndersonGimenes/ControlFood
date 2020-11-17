using ControlFood.Repository.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlFood.Repository.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder
                .ToTable("Cliente");

            builder
                .HasKey(x => x.Id)
                .HasName("Pk_cliente_id");

            builder
              .Property(x => x.DataCadastro)
              .HasColumnType("date")
              .IsRequired();

            builder
               .Property(x => x.DataAlteracao)
               .HasColumnType("date");

            builder
                .Property(x => x.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .Property(x => x.Cpf)
                .HasColumnType("varchar(14)");

            builder
                .Property(x => x.TelefoneFixo)
                .HasColumnType("varchar(14)");

            builder
                .Property(x => x.TelefoneCelular)
                .HasColumnType("varchar(14)");

            builder
                .Property(x => x.Email)
                .HasColumnType("varchar(200)");

            builder
                .Property(x => x.DataNascimento)
                .HasColumnType("date");
        }
    }
}
