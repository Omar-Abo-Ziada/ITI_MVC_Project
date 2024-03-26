using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstitueProject.Migrations
{
    /// <inheritdoc />
    public partial class insertExtravalues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Adress", "Salary" },
                values: new object[] { "Sohag", 20000m });

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Adress", "Salary" },
                values: new object[] { "Assiut", 15000m });

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Adress", "Salary" },
                values: new object[] { "Alex", 10000m });

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Adress", "Salary" },
                values: new object[] { "Cairo", 35000m });

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Adress", "Salary" },
                values: new object[] { "Aswan", 25000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Adress", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Adress", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Adress", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Adress", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Adress", "Salary" },
                values: new object[] { null, null });
        }
    }
}
