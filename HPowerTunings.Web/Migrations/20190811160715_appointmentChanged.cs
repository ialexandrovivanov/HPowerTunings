using Microsoft.EntityFrameworkCore.Migrations;

namespace HPowerTunings.Web.Migrations
{
    public partial class appointmentChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegNumber",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegNumber",
                table: "Appointments");
        }
    }
}
