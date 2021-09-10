using Microsoft.EntityFrameworkCore.Migrations;

namespace OP_WebApi.Migrations
{
    public partial class op14000618A7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "Warehouse_Request_History");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "Warehouse_Request");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "Warehouse_Remittance_History");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "Warehouse_Remittance");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "User");

            migrationBuilder.DropColumn(
                name: "End_DateTime_mi",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "Proforma");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "Order_History");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "LoginHistory");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "File");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "End_DateTime_mi",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "Collection_Action_History");

            migrationBuilder.DropColumn(
                name: "DateTime_mi",
                table: "Collection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "Warehouse_Request_History",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "Warehouse_Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "Warehouse_Remittance_History",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "Warehouse_Remittance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "End_DateTime_mi",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "Proforma",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "Order_History",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "LoginHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "File",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "End_DateTime_mi",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "Collection_Action_History",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime_mi",
                table: "Collection",
                nullable: true);
        }
    }
}
