using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class ReseedSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aaaec189-3a17-4f73-b424-0afa79ef8d57", "AQAAAAIAAYagAAAAEOQRaxhQb/XlUPeI1AIAjuqRL+izml40pmpL5L8QbHHARQ+2bxGUdRZg3Ol4pgGvqQ==", "f9becea1-29ce-4335-9734-457d0b839115" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8485c6c4-ea83-4d91-be8d-1ecb059be6e4", "AQAAAAIAAYagAAAAEOLbbiXf6gSe3/21uUKzWNDVM2ZYQ8/tOBXUanLJnfRD+Himu9IQDl28t6DZoFguQQ==", "fc98bcff-e079-4a49-9359-309ac407a21c" });
        }
    }
}
