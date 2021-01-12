using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ControlFood.Repository.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_categoria_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataAlteracao = table.Column<DateTime>(type: "date", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(14)", nullable: true),
                    TelefoneFixo = table.Column<string>(type: "varchar(14)", nullable: true),
                    TelefoneCelular = table.Column<string>(type: "varchar(14)", nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_cliente_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataAlteracao = table.Column<DateTime>(type: "date", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<string>(type: "Varchar(200)", nullable: false),
                    IndicadorItemCozinha = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IndicadorItemBar = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_subCategoria_id", x => x.Id);
                    table.ForeignKey(
                        name: "SubCategoria_Categoria",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataAlteracao = table.Column<DateTime>(type: "date", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    IndetificadorUnicoCliente = table.Column<int>(nullable: false),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(8)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(500)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(50)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(20)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(2)", nullable: true),
                    InfoApartamentoCondominio = table.Column<string>(type: "varchar(100)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_endereco_id", x => x.Id);
                    table.ForeignKey(
                        name: "cliente_endereco",
                        column: x => x.IndetificadorUnicoCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataAlteracao = table.Column<DateTime>(type: "date", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    CodigoInterno = table.Column<string>(type: "varchar(50)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    ValorVenda = table.Column<decimal>(type: "decimal", nullable: false),
                    SubCategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_produto_id", x => x.Id);
                    table.ForeignKey(
                        name: "Produto_SubCategoria",
                        column: x => x.SubCategoriaId,
                        principalTable: "SubCategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Estoque",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataAlteracao = table.Column<DateTime>(type: "date", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    DataValidade = table.Column<DateTime>(type: "date", nullable: false),
                    ValorCompraUnidade = table.Column<decimal>(type: "decimal", nullable: false, defaultValue: 0m),
                    ValorCompraTotal = table.Column<decimal>(type: "decimal", nullable: false),
                    IdProduto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_estoque_id", x => x.Id);
                    table.ForeignKey(
                        name: "Estoque_Produto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_IndetificadorUnicoCliente",
                table: "Endereco",
                column: "IndetificadorUnicoCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_IdProduto",
                table: "Estoque",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_SubCategoriaId",
                table: "Produto",
                column: "SubCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoria_CategoriaId",
                table: "SubCategoria",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Estoque");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "SubCategoria");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
