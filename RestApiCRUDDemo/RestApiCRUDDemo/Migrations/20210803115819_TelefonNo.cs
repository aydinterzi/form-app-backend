using Microsoft.EntityFrameworkCore.Migrations;

namespace RestApiCRUDDemo.Migrations
{
    public partial class TelefonNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TelefonNo",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelefonNo",
                table: "Employees");
        }
    }
}
