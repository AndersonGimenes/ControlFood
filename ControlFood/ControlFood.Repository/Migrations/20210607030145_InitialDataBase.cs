using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlFood.Repository.Migrations
{
    public partial class InitialDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adicional",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataAlteracao = table.Column<DateTime>(type: "date", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(200)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_adicional_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                name: "ProdutoVenda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataAlteracao = table.Column<DateTime>(type: "date", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    CodigoInterno = table.Column<string>(type: "varchar(50)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    ValorVenda = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_produto_id", x => x.Id);
                    table.ForeignKey(
                        name: "Produto_Categoria",
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                name: "ProdutoAdicional",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(nullable: false),
                    AdicionalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoAdicional", x => new { x.AdicionalId, x.ProdutoId });
                    table.ForeignKey(
                        name: "Adicional_Produto",
                        column: x => x.AdicionalId,
                        principalTable: "Adicional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Produto_Adicional",
                        column: x => x.ProdutoId,
                        principalTable: "ProdutoVenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_IndetificadorUnicoCliente",
                table: "Endereco",
                column: "IndetificadorUnicoCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoAdicional_ProdutoId",
                table: "ProdutoAdicional",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoVenda_CategoriaId",
                table: "ProdutoVenda",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "ProdutoAdicional");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Adicional");

            migrationBuilder.DropTable(
                name: "ProdutoVenda");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
