using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogic.Identity.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDireccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Departamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionCalle = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
