using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBettingSystem.Web.Migrations
{
    public partial class UpdatedBet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 22, 17, 14, 36, 562, DateTimeKind.Utc).AddTicks(2147),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 22, 17, 8, 40, 529, DateTimeKind.Utc).AddTicks(9561));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 22, 17, 8, 40, 529, DateTimeKind.Utc).AddTicks(9561),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 22, 17, 14, 36, 562, DateTimeKind.Utc).AddTicks(2147));
        }
    }
}
