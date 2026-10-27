using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaVirtual.Migrations
{
    /// <inheritdoc />
    public partial class NovaTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProdutosBaixados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoModelId = table.Column<int>(type: "int", nullable: false),
                    DataDaBaixa = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosBaixados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutosBaixados_Produtos_ProdutoModelId",
                        column: x => x.ProdutoModelId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nome",
                value: "Calçados");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosBaixados_ProdutoModelId",
                table: "ProdutosBaixados",
                column: "ProdutoModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutosBaixados");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nome",
                value: "Causados");
        }
    }
}
