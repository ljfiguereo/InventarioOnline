using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioOnline.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductoMarcaForeignKey_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Marcas_CategoriaId",
                table: "Productos");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MarcaId",
                table: "Productos",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Marcas_MarcaId",
                table: "Productos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Marcas_MarcaId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_MarcaId",
                table: "Productos");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Marcas_CategoriaId",
                table: "Productos",
                column: "CategoriaId",
                principalTable: "Marcas",
                principalColumn: "Id");
        }
    }
}
