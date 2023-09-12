using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CupiParqueadero.Migrations
{
    /// <inheritdoc />
    public partial class MigracionIntento2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Placa",
                table: "Vehiculos",
                newName: "Plate");

            migrationBuilder.RenameColumn(
                name: "Marca",
                table: "Vehiculos",
                newName: "Make");

            migrationBuilder.RenameColumn(
                name: "FechaEntrada",
                table: "Vehiculos",
                newName: "EntryDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Plate",
                table: "Vehiculos",
                newName: "Placa");

            migrationBuilder.RenameColumn(
                name: "Make",
                table: "Vehiculos",
                newName: "Marca");

            migrationBuilder.RenameColumn(
                name: "EntryDate",
                table: "Vehiculos",
                newName: "FechaEntrada");
        }
    }
}
