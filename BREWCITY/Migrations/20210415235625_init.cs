using Microsoft.EntityFrameworkCore.Migrations;

namespace BREWCITY.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Beers_BeerId",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_BeerId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "BeerId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<string>(
                name: "ShoppingCartId",
                table: "ShoppingCarts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Beers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Beers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageThumbnailUrl",
                table: "Beers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Beers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnSale",
                table: "Beers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "ShoppingCartId");

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    ShoppingCartItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShoppingCartId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BeerId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.ShoppingCartItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "BeerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_BeerId",
                table: "ShoppingCartItems",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "ImageThumbnailUrl",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "OnSale",
                table: "Beers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BeerId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Beers",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_BeerId",
                table: "ShoppingCarts",
                column: "BeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Beers_BeerId",
                table: "ShoppingCarts",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "BeerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
