using Microsoft.EntityFrameworkCore.Migrations;

namespace BREWCITY.Migrations
{
    public partial class attemptingtofixpendingdatabasechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TempCarts_Beers_BeerId",
                table: "TempCarts");

            migrationBuilder.DropIndex(
                name: "IX_TempCarts_BeerId",
                table: "TempCarts");

            migrationBuilder.DropColumn(
                name: "BeerId",
                table: "TempCarts");

            migrationBuilder.AddColumn<int>(
                name: "TempCartId",
                table: "Beers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beers_TempCartId",
                table: "Beers",
                column: "TempCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_TempCarts_TempCartId",
                table: "Beers",
                column: "TempCartId",
                principalTable: "TempCarts",
                principalColumn: "TempCartId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_TempCarts_TempCartId",
                table: "Beers");

            migrationBuilder.DropIndex(
                name: "IX_Beers_TempCartId",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "TempCartId",
                table: "Beers");

            migrationBuilder.AddColumn<int>(
                name: "BeerId",
                table: "TempCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TempCarts_BeerId",
                table: "TempCarts",
                column: "BeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TempCarts_Beers_BeerId",
                table: "TempCarts",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "BeerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
