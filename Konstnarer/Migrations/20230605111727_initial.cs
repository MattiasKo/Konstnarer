using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konstnarer.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("376add53-fded-4062-b464-7689453d2175"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("12e8afbd-3f42-4469-a3d6-e46d98a42ee0"));
        }
    }
}
