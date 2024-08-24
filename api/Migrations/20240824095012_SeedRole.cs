using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61154caf-d511-4411-8bf5-38aee83fe8bd", null, "Alek", "ALEK" },
                    { "ab4d84e9-9b3c-4420-a67a-33519e072f9c", null, "Admin", "ADMIN" },
                    { "e52dac55-63e9-4399-80c7-553858878cd8", null, "Shota", "SHOTA" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61154caf-d511-4411-8bf5-38aee83fe8bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab4d84e9-9b3c-4420-a67a-33519e072f9c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52dac55-63e9-4399-80c7-553858878cd8");
        }
    }
}
