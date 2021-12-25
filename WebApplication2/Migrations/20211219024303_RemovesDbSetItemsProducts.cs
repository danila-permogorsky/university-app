using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final.Migrations
{
    public partial class RemovesDbSetItemsProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemProducts_Items_ItemId",
                table: "ItemProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemProducts_Products_ProductId",
                table: "ItemProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemProducts",
                table: "ItemProducts");

            migrationBuilder.RenameTable(
                name: "ItemProducts",
                newName: "ItemProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ItemProducts_ProductId",
                table: "ItemProduct",
                newName: "IX_ItemProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemProduct",
                table: "ItemProduct",
                columns: new[] { "ItemId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemProduct_Items_ItemId",
                table: "ItemProduct",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemProduct_Products_ProductId",
                table: "ItemProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemProduct_Items_ItemId",
                table: "ItemProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemProduct_Products_ProductId",
                table: "ItemProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemProduct",
                table: "ItemProduct");

            migrationBuilder.RenameTable(
                name: "ItemProduct",
                newName: "ItemProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ItemProduct_ProductId",
                table: "ItemProducts",
                newName: "IX_ItemProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemProducts",
                table: "ItemProducts",
                columns: new[] { "ItemId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemProducts_Items_ItemId",
                table: "ItemProducts",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemProducts_Products_ProductId",
                table: "ItemProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
