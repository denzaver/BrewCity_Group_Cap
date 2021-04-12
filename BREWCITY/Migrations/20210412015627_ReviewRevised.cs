using Microsoft.EntityFrameworkCore.Migrations;

namespace BREWCITY.Migrations
{
    public partial class ReviewRevised : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24a3a34b-fa92-4709-b43c-07d15f173e8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd478bae-f02c-4926-8261-29aa25fba35c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed621e53-f343-4791-ba7c-76c67d6297a4");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Reviews");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "835af453-10bb-4da8-aa70-ab4030786347", "1ecdc877-96b2-4e11-bc9b-0d16b03c7d3b", "Brewery", "BREWERY" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8955f368-f487-4a82-8b94-2a909a4ae9fd", "bc7f0d67-7be1-41b5-b34d-46c72a9a1e01", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35255f5a-0683-4a45-b4eb-729492674885", "762b48ce-a1de-40b0-9292-2d033bf41b64", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35255f5a-0683-4a45-b4eb-729492674885");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "835af453-10bb-4da8-aa70-ab4030786347");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8955f368-f487-4a82-8b94-2a909a4ae9fd");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd478bae-f02c-4926-8261-29aa25fba35c", "a158b2e0-c349-4515-a86d-6a019fd08b23", "Brewery", "BREWERY" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ed621e53-f343-4791-ba7c-76c67d6297a4", "f3afc328-acfd-4926-a7bf-257350038edc", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "24a3a34b-fa92-4709-b43c-07d15f173e8b", "9958c3fa-105d-4f53-8789-07ef975fb353", "Admin", "ADMIN" });
        }
    }
}
