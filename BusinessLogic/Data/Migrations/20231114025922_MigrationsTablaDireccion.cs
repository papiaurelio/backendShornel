using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogic.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsTablaDireccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DireccionEnvio_Ciudad",
                table: "OrdenCompras");

            migrationBuilder.DropColumn(
                name: "DireccionEnvio_Departamento",
                table: "OrdenCompras");

            migrationBuilder.DropColumn(
                name: "DireccionEnvio_DireccionCalle",
                table: "OrdenCompras");

            migrationBuilder.AddColumn<int>(
                name: "DireccionId",
                table: "OrdenCompras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DireccionCalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Departamento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenCompras_DireccionId",
                table: "OrdenCompras",
                column: "DireccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenCompras_Direccion_DireccionId",
                table: "OrdenCompras",
                column: "DireccionId",
                principalTable: "Direccion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenCompras_Direccion_DireccionId",
                table: "OrdenCompras");

            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropIndex(
                name: "IX_OrdenCompras_DireccionId",
                table: "OrdenCompras");

            migrationBuilder.DropColumn(
                name: "DireccionId",
                table: "OrdenCompras");

            migrationBuilder.AddColumn<string>(
                name: "DireccionEnvio_Ciudad",
                table: "OrdenCompras",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DireccionEnvio_Departamento",
                table: "OrdenCompras",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DireccionEnvio_DireccionCalle",
                table: "OrdenCompras",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
