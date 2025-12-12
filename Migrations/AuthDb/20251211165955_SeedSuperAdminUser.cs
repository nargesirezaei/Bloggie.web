using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class SeedSuperAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "549a4bd2-903e-412d-a63a-d79b2fe7865d", "AQAAAAIAAYagAAAAEJMyVBk28nRRs35RLCk5IIH/WjpUaagVc/y2AB70oCqAW6/L318FvLBXHe2wUJbq7w==", "d1a22c0c-336f-4fc8-bb3d-ba051e1d6328" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebe27fdf-2761-4128-b6c0-8aa95012ef80", "AQAAAAIAAYagAAAAELmXIL/qlWMbVL8AixtGnO4JCo+AcYeN5ETKLnGtT1GW6RWsXXz00xkK8RtaB8Z/aw==", "374e714d-5397-4c4d-a6fa-d0011792ca37" });
        }
    }
}
