using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class RefactoringadminRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "IdentityDbContext", "7f3e7cf7-8510-4e62-b772-e81dc008a57e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "IdentityDbContext");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9A82FCFD-6778-463D-810B-80A7BFC01735", "9A82FCFD-6778-463D-810B-80A7BFC01735", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "977f4d3c-e87b-4836-a7e0-5a64d068f8bb", "AQAAAAIAAYagAAAAEOtRiPTk0NEHm3q7rZDZOnkjGVSKTvIFp+pAHOTb5tjPRmwEwhG8WtixPbFOXcujHA==", "7d89a521-f2e3-4718-83c1-c105f7ec3fad" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9A82FCFD-6778-463D-810B-80A7BFC01735", "7f3e7cf7-8510-4e62-b772-e81dc008a57e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9A82FCFD-6778-463D-810B-80A7BFC01735", "7f3e7cf7-8510-4e62-b772-e81dc008a57e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9A82FCFD-6778-463D-810B-80A7BFC01735");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "IdentityDbContext", "IdentityDbContext", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51079c59-3c74-4ea7-8b48-de9778939e9d", "AQAAAAIAAYagAAAAEPrV3q5Sd3bFF/TToKF9jjiM4bLiQFPWNKcY/zWaojbj+POKZoF1i8YDBSbvmQovtw==", "c8b8a043-f3ca-410d-9830-a2a1adaaf1ff" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "IdentityDbContext", "7f3e7cf7-8510-4e62-b772-e81dc008a57e" });
        }
    }
}
