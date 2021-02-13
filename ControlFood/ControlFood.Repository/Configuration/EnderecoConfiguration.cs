//using ControlFood.Repository.Entidades;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace ControlFood.Repository.Configuration
//{
//    class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
//    {
//        public void Configure(EntityTypeBuilder<Endereco> builder)
//        {
//            builder
//                .ToTable("Endereco");

//            builder
//                .HasKey(x => x.Id)
//                .HasName("Pk_endereco_id");

//            builder
//              .Property(x => x.DataCadastro)
//              .HasColumnType("date")
//              .IsRequired();

//            builder
//               .Property(x => x.DataAlteracao)
//               .HasColumnType("date");

//            builder
//                .Property(x => x.Numero)
//                .HasColumnType("varchar(10)");

//            builder
//                .Property(x => x.Cep)
//                .HasColumnType("varchar(8)");

//            builder
//                .Property(x => x.Logradouro)
//                .HasColumnType("varchar(500)");

//            builder
//                .Property(x => x.Bairro)
//                .HasColumnType("varchar(50)");

//            builder
//                .Property(x => x.Cidade)
//                .HasColumnType("varchar(20)");

//            builder
//                .Property(x => x.Estado)
//                .HasColumnType("varchar(2)");

//            builder
//                .Property(x => x.InfoApartamentoCondominio)
//                .HasColumnType("varchar(100)");

//            builder
//                .Property(x => x.Complemento)
//                .HasColumnType("varchar(250)");

//            builder
//                .HasOne(x => x.Cliente)
//                .WithMany(x => x.Enderecos)
//                .HasForeignKey(x => x.IndetificadorUnicoCliente)
//                .HasConstraintName("cliente_endereco")
//                .OnDelete(DeleteBehavior.Restrict);
//        }
//    }
//}
