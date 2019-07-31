using Microsoft.EntityFrameworkCore.Migrations;

namespace HPowerTunings.Web.Migrations
{
    public partial class IsReapirPendingNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReairPanding",
                table: "Repairs",
                newName: "IsRepairPanding");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRepairPanding",
                table: "Repairs",
                newName: "IsReairPanding");
        }
    }
}
