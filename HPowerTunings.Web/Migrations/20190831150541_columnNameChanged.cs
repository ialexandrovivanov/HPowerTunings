using Microsoft.EntityFrameworkCore.Migrations;

namespace HPowerTunings.Web.Migrations
{
    public partial class columnNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "CarsForParts",
                newName: "RegNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegNumber",
                table: "CarsForParts",
                newName: "RegistrationNumber");
        }
    }
}
