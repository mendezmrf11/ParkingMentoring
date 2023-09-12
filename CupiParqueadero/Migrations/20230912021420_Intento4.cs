using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CupiParqueadero.Migrations
{
    /// <inheritdoc />
    public partial class Intento4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehiculos",
                table: "Vehiculos");

            migrationBuilder.RenameTable(
                name: "Vehiculos",
                newName: "Vehicles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehiculos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehiculos",
                table: "Vehiculos",
                column: "Id");
        }
    }
}
