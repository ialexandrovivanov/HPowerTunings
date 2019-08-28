using Microsoft.EntityFrameworkCore.Migrations;

namespace HPowerTunings.Web.Migrations
{
    public partial class changedColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartName",
                table: "PartsFromCars",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "CarsForParts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "CarsForParts");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PartsFromCars",
                newName: "PartName");
        }
    }
}
