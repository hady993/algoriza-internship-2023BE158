using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AlterSeedAdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e5370727-9eea-4c77-a071-00ebfd760617", "6cd4f6f3-f566-46c2-9546-b5530964a22f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cd4f6f3-f566-46c2-9546-b5530964a22f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba1350bc-84c9-4582-8fa6-987bff1c4928", "AQAAAAIAAYagAAAAEC/b1o1lqvsx4SBJi+0i/nMERz1vxOcIRGmwjpl/FZNE3W9ZIrflJddWvKhF9sXfMg==", "ae904397-1fc1-43a6-8671-86aa9091640a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e5370727-9eea-4c77-a071-00ebfd760617", "6cd4f6f3-f566-46c2-9546-b5530964a22f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cd4f6f3-f566-46c2-9546-b5530964a22f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1325166-6d93-408a-a489-6a4f30038677", "AQAAAAIAAYagAAAAEOJQ5DVyAnQQ2MaDRPI2j6keOErKtnAeWiE9iQVlqooeUxlpUuSerrUofynZtlVAww==", "06f8de6d-0ec9-465c-bd7e-9bf606363598" });
        }
    }
}
