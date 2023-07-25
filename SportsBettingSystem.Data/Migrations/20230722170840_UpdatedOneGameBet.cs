using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBettingSystem.Web.Migrations
{
    public partial class UpdatedOneGameBet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prediction",
                table: "Bets");

            migrationBuilder.AddColumn<bool>(
                name: "isFinished",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Prediction",
                table: "GameBet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "gamesFinished",
                table: "Bets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 22, 17, 8, 40, 529, DateTimeKind.Utc).AddTicks(9561),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 16, 18, 16, 735, DateTimeKind.Utc).AddTicks(7866));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFinished",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Prediction",
                table: "GameBet");

            migrationBuilder.DropColumn(
                name: "gamesFinished",
                table: "Bets");

            migrationBuilder.AddColumn<int>(
                name: "Prediction",
                table: "Bets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 16, 18, 16, 735, DateTimeKind.Utc).AddTicks(7866),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 22, 17, 8, 40, 529, DateTimeKind.Utc).AddTicks(9561));
        }
    }
}
