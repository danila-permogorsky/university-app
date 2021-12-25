using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Final.Migrations
{
    public partial class AddedAttributesForRelationMigratio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemProduct",
                table: "ItemProduct");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ItemProduct",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemProduct",
                table: "ItemProduct",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemProduct_ItemId",
                table: "ItemProduct",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemProduct",
                table: "ItemProduct");

            migrationBuilder.DropIndex(
                name: "IX_ItemProduct_ItemId",
                table: "ItemProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItemProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemProduct",
                table: "ItemProduct",
                columns: new[] { "ItemId", "ProductId" });
        }
    }
}
