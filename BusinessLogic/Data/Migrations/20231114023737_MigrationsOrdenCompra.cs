using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogic.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsOrdenCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdenCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdComprador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorreoComprador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaOrdenCompra = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DireccionEnvio_DireccionCalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionEnvio_Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionEnvio_Departamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Envio = table.Column<bool>(type: "bit", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PagoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioEnvio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenCompras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdenItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoOrdenado_ProductoOrdenadoId = table.Column<int>(type: "int", nullable: true),
                    ProductoOrdenado_ProductoNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductoOrdenado_ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    OrdenComprasId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenItem_OrdenCompras_OrdenComprasId",
                        column: x => x.OrdenComprasId,
                        principalTable: "OrdenCompras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenItem_OrdenComprasId",
                table: "OrdenItem",
                column: "OrdenComprasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenItem");

            migrationBuilder.DropTable(
                name: "OrdenCompras");
        }
    }
}
