using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaVirtual.Migrations
{
    /// <inheritdoc />
    public partial class PreservarHistoricoDeVendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosBaixados_Produtos_ProdutoModelId",
                table: "ProdutosBaixados");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoModelId",
                table: "ProdutosBaixados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "NomeCategoria",
                table: "ProdutosBaixados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeProduto",
                table: "ProdutosBaixados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ValorProduto",
                table: "ProdutosBaixados",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.Sql(@"
                UPDATE pb
                SET pb.NomeProduto = p.Nome,
                    pb.NomeCategoria = c.Nome,
                    pb.ValorProduto = p.Valor
                FROM ProdutosBaixados pb
                INNER JOIN Produtos p ON p.Id = pb.ProdutoModelId
                INNER JOIN Categorias c ON c.Id = p.CategoriaId;");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosBaixados_Produtos_ProdutoModelId",
                table: "ProdutosBaixados",
                column: "ProdutoModelId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosBaixados_Produtos_ProdutoModelId",
                table: "ProdutosBaixados");

            migrationBuilder.DropColumn(
                name: "NomeCategoria",
                table: "ProdutosBaixados");

            migrationBuilder.DropColumn(
                name: "NomeProduto",
                table: "ProdutosBaixados");

            migrationBuilder.DropColumn(
                name: "ValorProduto",
                table: "ProdutosBaixados");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoModelId",
                table: "ProdutosBaixados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosBaixados_Produtos_ProdutoModelId",
                table: "ProdutosBaixados",
                column: "ProdutoModelId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
