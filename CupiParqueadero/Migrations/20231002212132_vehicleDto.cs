using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CupiParqueadero.Migrations
{
    /// <inheritdoc />
    public partial class vehicleDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPayable",
                table: "Vehicles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayDate",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPayable",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "PayDate",
                table: "Vehicles");
        }
    }
}
