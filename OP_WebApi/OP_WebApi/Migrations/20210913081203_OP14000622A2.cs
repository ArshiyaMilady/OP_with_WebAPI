using Microsoft.EntityFrameworkCore.Migrations;

namespace OP_WebApi.Migrations
{
    public partial class OP14000622A2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File_Index",
                table: "Item_File",
                newName: "File_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File_Id",
                table: "Item_File",
                newName: "File_Index");
        }
    }
}
