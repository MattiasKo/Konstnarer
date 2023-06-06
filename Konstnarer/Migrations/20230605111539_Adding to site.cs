using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konstnarer.Migrations
{
    /// <inheritdoc />
    public partial class Addingtosite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("12e8afbd-3f42-4469-a3d6-e46d98a42ee0"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: new Guid("2ef42f24-91b8-444f-919e-00dded839fec"));
        }
    }
}
