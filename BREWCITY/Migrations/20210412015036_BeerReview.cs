using Microsoft.EntityFrameworkCore.Migrations;

namespace BREWCITY.Migrations
{
    public partial class BeerReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5cc6b7bf-b205-4c46-875e-a6104890e1ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89cbec7d-5cf8-4022-ba12-2fcb37ab6b22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eecf4d73-c432-4f07-84e6-5e212f81a11e");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eecf4d73-c432-4f07-84e6-5e212f81a11e", "2242ea70-d65a-492e-be94-164ef87a9bf0", "Brewery", "BREWERY" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89cbec7d-5cf8-4022-ba12-2fcb37ab6b22", "71462a8c-86b1-4f25-9504-a695dc493cff", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5cc6b7bf-b205-4c46-875e-a6104890e1ee", "0bed83ab-1ae7-4930-861f-fea445b8fef5", "Admin", "ADMIN" });
        }
    }
}
