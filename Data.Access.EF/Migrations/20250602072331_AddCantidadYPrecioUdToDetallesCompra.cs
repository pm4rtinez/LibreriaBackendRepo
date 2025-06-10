using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Access.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddCantidadYPrecioUdToDetallesCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "DetallesCompra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUd",
                table: "DetallesCompra",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "DetallesCompra");

            migrationBuilder.DropColumn(
                name: "PrecioUd",
                table: "DetallesCompra");
        }
    }
}