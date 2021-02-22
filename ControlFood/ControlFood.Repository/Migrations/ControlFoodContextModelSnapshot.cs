﻿// <auto-generated />
using System;
using ControlFood.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ControlFood.Repository.Migrations
{
    [DbContext(typeof(ControlFoodContext))]
    partial class ControlFoodContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ControlFood.Repository.Entidades.Adicional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("date");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id")
                        .HasName("Pk_adicional_id");

                    b.ToTable("Adicional");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("date");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id")
                        .HasName("Pk_categoria_id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("CodigoInterno")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DataValidade")
                        .HasColumnType("date");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("ValorCompra")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("ValorVenda")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id")
                        .HasName("Pk_produto_id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.ProdutoAdicional", b =>
                {
                    b.Property<int>("AdicionalId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.HasKey("AdicionalId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ProdutoAdicional");
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.Produto", b =>
                {
                    b.HasOne("ControlFood.Repository.Entidades.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .HasConstraintName("Produto_Categoria")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ControlFood.Repository.Entidades.ProdutoAdicional", b =>
                {
                    b.HasOne("ControlFood.Repository.Entidades.Adicional", "Adicional")
                        .WithMany("Produtos")
                        .HasForeignKey("AdicionalId")
                        .HasConstraintName("Adicional_Produto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControlFood.Repository.Entidades.Produto", "Produto")
                        .WithMany("Adicionais")
                        .HasForeignKey("ProdutoId")
                        .HasConstraintName("Produto_Adicional")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
