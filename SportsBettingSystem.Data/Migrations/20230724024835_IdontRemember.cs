using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBettingSystem.Web.Migrations
{
    public partial class IdontRemember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gamesFinished",
                table: "Bets",
                newName: "IsDone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 24, 2, 48, 34, 262, DateTimeKind.Utc).AddTicks(3794),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 22, 17, 14, 36, 562, DateTimeKind.Utc).AddTicks(2147));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDone",
                table: "Bets",
                newName: "gamesFinished");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 22, 17, 14, 36, 562, DateTimeKind.Utc).AddTicks(2147),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 24, 2, 48, 34, 262, DateTimeKind.Utc).AddTicks(3794));
        }
    }
}
