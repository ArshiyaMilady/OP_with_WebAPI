using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OP_WebApi.Migrations
{
    public partial class op14000618A4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "End_DateTime_mi",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "End_DateTime_mi",
                table: "Company",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End_DateTime_mi",
                table: "User");

            migrationBuilder.DropColumn(
                name: "End_DateTime_mi",
                table: "Company");
        }
    }
}
