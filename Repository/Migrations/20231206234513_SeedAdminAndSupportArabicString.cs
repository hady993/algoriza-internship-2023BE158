using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminAndSupportArabicString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Specialization",
                type: "nvarchar(max)",
                nullable: false,
                collation: "Arabic_CI_AI",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountType", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImage", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6cd4f6f3-f566-46c2-9546-b5530964a22f", 0, 0, "defd5455-6dcd-443e-a68a-446002923425", new DateOnly(2000, 10, 15), "admin@example.com", false, "Ahmed Admin", 0, false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOUrQjEkcFhCej5zPFezwZBxGak6olCSyMb+BoFL8hqQS4YcfZGke7Jbkj296YtE9w==", "01029108133", false, null, "23d86a38-bb73-428e-bba9-d461f29c5e4c", false, "admin@example.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cd4f6f3-f566-46c2-9546-b5530964a22f");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Specialization",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldCollation: "Arabic_CI_AI");
        }
    }
}
