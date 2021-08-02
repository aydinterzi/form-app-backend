using Microsoft.EntityFrameworkCore.Migrations;

namespace RestApiCRUDDemo.Migrations
{
    public partial class Inada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GirisTarihi",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GirisTarihi",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "No",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Employees");
        }
    }
}
