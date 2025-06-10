using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Access.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddTieneComprobanteToReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TieneComprobante",
                table: "Reservas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TieneComprobante",
                table: "Reservas");
        }
    }
}
