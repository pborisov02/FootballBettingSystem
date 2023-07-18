using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBettingSystem.Web.Migrations
{
    public partial class changed_the_games_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayGoals",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeGoals",
                table: "Games");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 17, 23, 24, 15, 169, DateTimeKind.Utc).AddTicks(9918),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 16, 21, 40, 20, 54, DateTimeKind.Utc).AddTicks(9640));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwayGoals",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeGoals",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 16, 21, 40, 20, 54, DateTimeKind.Utc).AddTicks(9640),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 17, 23, 24, 15, 169, DateTimeKind.Utc).AddTicks(9918));
        }
    }
}
