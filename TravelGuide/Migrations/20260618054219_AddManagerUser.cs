using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelGuide.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AgencyId", "AvatarUrl", "Email", "FullName", "IsBlocked", "PasswordHash", "Phone", "RegistrationDate", "Role" },
                values: new object[] { 2, null, null, "manager@travelguide.com", "Менеджер Туров", false, "$2a$11$placeholderWillBeUpdatedOnFirstRun", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
