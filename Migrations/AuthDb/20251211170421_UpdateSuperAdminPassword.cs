using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class UpdateSuperAdminPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ccefefdd-f84d-4487-ad74-649e3efe06d4", "AQAAAAIAAYagAAAAELbI6qh+bk3VOANn7qbj2F5M7cSKdNkUURVP34Aaz9UgovK71JBRXi13Dw5suda+YQ==", "dedf3261-27c6-412e-82c7-a01dd05e2ea6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "549a4bd2-903e-412d-a63a-d79b2fe7865d", "AQAAAAIAAYagAAAAEJMyVBk28nRRs35RLCk5IIH/WjpUaagVc/y2AB70oCqAW6/L318FvLBXHe2wUJbq7w==", "d1a22c0c-336f-4fc8-bb3d-ba051e1d6328" });
        }
    }
}
