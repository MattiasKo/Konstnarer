using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konstnarer.Migrations
{
    /// <inheritdoc />
    public partial class Descriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("e0daff77-3844-4559-a587-6cc38754b539"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("ba45a93e-5edf-4148-ba03-453fb36015bc"));
        }
    }
}
