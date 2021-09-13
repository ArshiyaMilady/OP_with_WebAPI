using Microsoft.EntityFrameworkCore.Migrations;

namespace OP_WebApi.Migrations
{
    public partial class OP14000622A3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Item_Id",
                table: "Item_File",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item_Id",
                table: "Item_File");
        }
    }
}
