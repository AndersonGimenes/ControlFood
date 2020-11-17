﻿// <auto-generated />
using System;
using ControlFood.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ControlFood.Repository.Migrations
{
    [DbContext(typeof(ControlFoodContext))]
    partial class ControlFoodContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ControlFood.Repository.Entidades.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("date");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id")
                        .HasName("Pk_categoria_id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TelefoneCelular")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("TelefoneFixo")
                        .HasColumnType("varchar(14)");

                    b.HasKey("Id")
                        .HasName("Pk_cliente_id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Cep")
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("date");

                    b.Property<string>("Estado")
                        .HasColumnType("varchar(2)");

                    b.Property<int>("IndetificadorUnicoCliente")
                        .HasColumnType("integer");

                    b.Property<string>("InfoApartamentoCondominio")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id")
                        .HasName("Pk_endereco_id");

                    b.HasIndex("IndetificadorUnicoCliente");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Estoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("date");

                    b.Property<int>("IdProduto")
                        .HasColumnType("integer");

                    b.Property<int>("Quantidade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<decimal>("ValorCompraTotal")
                        .HasColumnType("decimal");

                    b.Property<decimal>("ValorCompraUnidade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal")
                        .HasDefaultValue(0m);

                    b.HasKey("Id")
                        .HasName("Pk_estoque_id");

                    b.HasIndex("IdProduto");

                    b.ToTable("Estoque");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CodigoInterno")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("date");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("SubCategoriaId")
                        .HasColumnType("integer");

                    b.Property<decimal>("ValorVenda")
                        .HasColumnType("decimal");

                    b.HasKey("Id")
                        .HasName("Pk_produto_id");

                    b.HasIndex("SubCategoriaId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.SubCategoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("date");

                    b.Property<bool>("IndicadorItemBar")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IndicadorItemCozinha")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("Varchar(200)");

                    b.HasKey("Id")
                        .HasName("Pk_subCategoria_id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("SubCategoria");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Endereco", b =>
                {
                    b.HasOne("ControlFood.Repository.Entidades.Cliente", "Cliente")
                        .WithMany("Enderecos")
                        .HasForeignKey("IndetificadorUnicoCliente")
                        .HasConstraintName("cliente_endereco")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Estoque", b =>
                {
                    b.HasOne("ControlFood.Repository.Entidades.Produto", "Produto")
                        .WithMany("Estoques")
                        .HasForeignKey("IdProduto")
                        .HasConstraintName("Estoque_Produto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Produto", b =>
                {
                    b.HasOne("ControlFood.Repository.Entidades.SubCategoria", "SubCategoria")
                        .WithMany("Produtos")
                        .HasForeignKey("SubCategoriaId")
                        .HasConstraintName("Produto_SubCategoria")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.SubCategoria", b =>
                {
                    b.HasOne("ControlFood.Repository.Entidades.Categoria", "Categoria")
                        .WithMany("SubCategorias")
                        .HasForeignKey("CategoriaId")
                        .HasConstraintName("SubCategoria_Categoria")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
