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

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id")
                        .HasName("Pk_categoria_id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Estoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("DataEntrada")
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

            modelBuilder.Entity("ControlFood.Repository.Entidades.Estoque", b =>
                {
                    b.HasOne("ControlFood.Repository.Entidades.Produto", "Produto")
                        .WithMany("Estoques")
                        .HasForeignKey("IdProduto")
                        .HasConstraintName("Estoque_Produto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Produto", b =>
                {
                    b.HasOne("ControlFood.Repository.Entidades.SubCategoria", "SubCategoria")
                        .WithMany("Produtos")
                        .HasForeignKey("SubCategoriaId")
                        .HasConstraintName("Produto_SubCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.SubCategoria", b =>
                {
                    b.HasOne("ControlFood.Repository.Entidades.Categoria", "Categoria")
                        .WithMany("SubCategorias")
                        .HasForeignKey("CategoriaId")
                        .HasConstraintName("SubCategoria_Categoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
