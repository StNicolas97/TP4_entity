using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP4_Identity_Web.Migrations
{
    /// <inheritdoc />
    public partial class Seederole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a1b2c3d4-0001-0000-0000-000000000000", "a1b2c3d4-0001-0000-0000-000000000000", "Roi", "ROI" },
                    { "a1b2c3d4-0002-0000-0000-000000000000", "a1b2c3d4-0002-0000-0000-000000000000", "Capitaine", "CAPITAINE" },
                    { "a1b2c3d4-0003-0000-0000-000000000000", "a1b2c3d4-0003-0000-0000-000000000000", "Habitant", "HABITANT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adresse", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nom", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surnom", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b1c2d3e4-0001-0000-0000-000000000000", 0, null, "b1c2d3e4-0001-0000-0000-000000000000", "aragorn@gondor.gov", true, false, null, "Elessar", "ARAGORN@GONDOR.GOV", "ARAGORN@GONDOR.GOV", "AQAAAAIAAYagAAAAEKk0ykdO9xAxtd+E/abSFFxEAHbBfYP9dcwup2JIMxozgtqH08xD5uDKP4WJL44lXA==", null, false, "b1c2d3e4-0001-0000-0000-000000000000", "Aragorn", false, "aragorn@gondor.gov" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "Peuple", "Gondor", "b1c2d3e4-0001-0000-0000-000000000000" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a1b2c3d4-0001-0000-0000-000000000000", "b1c2d3e4-0001-0000-0000-000000000000" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-0002-0000-0000-000000000000");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-0003-0000-0000-000000000000");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a1b2c3d4-0001-0000-0000-000000000000", "b1c2d3e4-0001-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-0001-0000-0000-000000000000");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b1c2d3e4-0001-0000-0000-000000000000");
        }
    }
}
