using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class SeedSpecializationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cd4f6f3-f566-46c2-9546-b5530964a22f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1325166-6d93-408a-a489-6a4f30038677", "AQAAAAIAAYagAAAAEOJQ5DVyAnQQ2MaDRPI2j6keOErKtnAeWiE9iQVlqooeUxlpUuSerrUofynZtlVAww==", "06f8de6d-0ec9-465c-bd7e-9bf606363598" });

            migrationBuilder.InsertData(
                table: "Specialization",
                columns: new[] { "Id", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "تخدير", "Anesthesiology" },
                    { 2, "قلب", "Cardiology" },
                    { 3, "جلدية", "Dermatology" },
                    { 4, "جراحة", "Surgery" },
                    { 5, "أعصاب", "Neurology" },
                    { 6, "أنف وأذن وحنجرة", "Otolaryngology" },
                    { 7, "أطفال", "Pediatrics" },
                    { 8, "علاج طبيعي", "PhysicalMedicine" },
                    { 9, "طب نفسي", "Psychiatry" },
                    { 10, "أشعة", "Radiology" },
                    { 11, "صدر", "Thoracic" },
                    { 12, "مسالك بولية", "Urology" },
                    { 13, "أوعية دموية", "Vascular" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Specialization",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cd4f6f3-f566-46c2-9546-b5530964a22f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "defd5455-6dcd-443e-a68a-446002923425", "AQAAAAIAAYagAAAAEOUrQjEkcFhCej5zPFezwZBxGak6olCSyMb+BoFL8hqQS4YcfZGke7Jbkj296YtE9w==", "23d86a38-bb73-428e-bba9-d461f29c5e4c" });
        }
    }
}
