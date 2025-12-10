using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebe27fdf-2761-4128-b6c0-8aa95012ef80", "AQAAAAIAAYagAAAAELmXIL/qlWMbVL8AixtGnO4JCo+AcYeN5ETKLnGtT1GW6RWsXXz00xkK8RtaB8Z/aw==", "374e714d-5397-4c4d-a6fa-d0011792ca37" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3e7cf7-8510-4e62-b772-e81dc008a57e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "977f4d3c-e87b-4836-a7e0-5a64d068f8bb", "AQAAAAIAAYagAAAAEOtRiPTk0NEHm3q7rZDZOnkjGVSKTvIFp+pAHOTb5tjPRmwEwhG8WtixPbFOXcujHA==", "7d89a521-f2e3-4718-83c1-c105f7ec3fad" });
        }
    }
}
