using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final.Migrations
{
    public partial class AddedStatusesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Products",
                newName: "AssemblyStatus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Items",
                newName: "InstallStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssemblyStatus",
                table: "Products",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "InstallStatus",
                table: "Items",
                newName: "Status");
        }
    }
}
