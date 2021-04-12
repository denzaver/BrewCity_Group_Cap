using Microsoft.EntityFrameworkCore.Migrations;

namespace BREWCITY.Migrations
{
    public partial class LiarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d7e5c8ea-cbc2-4d66-81cf-efae5fefd137", "f97e27aa-a70d-4603-b296-a3f6859dd253", "Brewery", "BREWERY" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e82de51-7342-41f0-bffa-ff154800a55b", "7bb9037c-277b-485d-a150-e6a8006a20eb", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2977ce1c-9089-4508-be3d-02e1cda9e07c", "351ab8f6-41be-426b-9974-3bc2f5a75bff", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2977ce1c-9089-4508-be3d-02e1cda9e07c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e82de51-7342-41f0-bffa-ff154800a55b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7e5c8ea-cbc2-4d66-81cf-efae5fefd137");

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
    }
}
