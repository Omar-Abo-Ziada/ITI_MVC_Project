using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstitueProject.Migrations
{
    /// <inheritdoc />
    public partial class temp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "1.jpg");

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "2.jpg");

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "3.jpg");

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "4.jpg");

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "5.jpg");

            migrationBuilder.UpdateData(
                table: "Trainee",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "6.jpg");

            migrationBuilder.UpdateData(
                table: "Trainee",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "7.jpg");

            migrationBuilder.UpdateData(
                table: "Trainee",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "8.jpg");

            migrationBuilder.UpdateData(
                table: "Trainee",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "9.jpg");

            migrationBuilder.UpdateData(
                table: "Trainee",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "10.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "~/images/1.jpg");

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "~/images/2.jpg");

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "~/images/3.jpg");

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "~/images/4.jpg");

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "~/images/5.jpg");

            migrationBuilder.UpdateData(
                table: "Trainee",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "~/images/1.jpg");

            migrationBuilder.UpdateData(
                table: "Trainee",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "~/images/2.jpg");

            migrationBuilder.UpdateData(
                table: "Trainee",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "~/images/3.jpg");

            migrationBuilder.UpdateData(
                table: "Trainee",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "~/images/4.jpg");

            migrationBuilder.UpdateData(
                table: "Trainee",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "~/images/5.jpg");
        }
    }
}
