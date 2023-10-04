using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CupiParqueadero.Migrations
{
    /// <inheritdoc />
    public partial class payVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Pay",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pay",
                table: "Vehicles");
        }
    }
}
