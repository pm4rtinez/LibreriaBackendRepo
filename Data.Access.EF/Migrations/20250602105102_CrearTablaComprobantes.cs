using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Access.EF.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaComprobantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComprobantesDevolucion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaId = table.Column<long>(type: "bigint", nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirmaImagen = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FechaGeneracion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprobantesDevolucion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComprobantesDevolucion_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComprobantesDevolucion_ReservaId",
                table: "ComprobantesDevolucion",
                column: "ReservaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComprobantesDevolucion");
        }
    }
}
