using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPowerTunings.Web.Migrations
{
    public partial class ChangesInPartAndRepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderedOn",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "ReceivedOn",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryRate",
                table: "Suppliers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryRate",
                table: "Suppliers");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderedOn",
                table: "Parts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedOn",
                table: "Parts",
                nullable: true);
        }
    }
}
