using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class SeedSuperAdminUserFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ef60c6b-839c-4273-96f7-31aa024047d0", "AQAAAAIAAYagAAAAEPCPFGsArH9E3W/zVg6SFvph/PidI181Df0NVNeGJAjj46rZ4FPVOb413Y2C13Lb2A==", "13dc2fe8-2d14-45a3-85d4-cc33c37763e1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aaaec189-3a17-4f73-b424-0afa79ef8d57", "AQAAAAIAAYagAAAAEOQRaxhQb/XlUPeI1AIAjuqRL+izml40pmpL5L8QbHHARQ+2bxGUdRZg3Ol4pgGvqQ==", "f9becea1-29ce-4335-9734-457d0b839115" });
        }
    }
}
