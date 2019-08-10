using Microsoft.EntityFrameworkCore.Migrations;

namespace HPowerTunings.Web.Migrations
{
    public partial class IsAppointmentStartedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAppointmentStarted",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAppointmentStarted",
                table: "Appointments");
        }
    }
}
