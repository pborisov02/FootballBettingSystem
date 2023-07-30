using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBettingSystem.Web.Migrations
{
    public partial class UpdateLeagueGameAndTeamEntitites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BadgeUrl",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "StadiumName",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Leagues");

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 30, 2, 19, 5, 73, DateTimeKind.Utc).AddTicks(7926),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 30, 1, 59, 39, 318, DateTimeKind.Utc).AddTicks(1993));

            migrationBuilder.CreateIndex(
                name: "IX_Games_LeagueId",
                table: "Games",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Leagues_LeagueId",
                table: "Games",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Leagues_LeagueId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_LeagueId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "BadgeUrl",
                table: "Teams",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StadiumName",
                table: "Teams",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Leagues",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 30, 1, 59, 39, 318, DateTimeKind.Utc).AddTicks(1993),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 30, 2, 19, 5, 73, DateTimeKind.Utc).AddTicks(7926));

            migrationBuilder.UpdateData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 1,
                column: "LogoUrl",
                value: "https://content.sportslogos.net/leagues/thumbs/130.gif");

            migrationBuilder.UpdateData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 2,
                column: "LogoUrl",
                value: "https://content.sportslogos.net/leagues/thumbs/132.gif");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BadgeUrl", "StadiumName" },
                values: new object[] { "https://content.sportslogos.net/logos/130/4016/thumbs/hy5fvvdkee83gg3r5ym22zr5o.gif", "Camp Nou" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BadgeUrl", "StadiumName" },
                values: new object[] { "https://content.sportslogos.net/logos/130/4017/thumbs/yfhezt5oyr0jbq29u4hp50w63.gif", "Santiago Bernabeu" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BadgeUrl", "StadiumName" },
                values: new object[] { "https://content.sportslogos.net/logos/132/4069/thumbs/rr72mhpas38h85jdw85neas5f.gif", "Allianz Arena" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BadgeUrl", "StadiumName" },
                values: new object[] { "https://content.sportslogos.net/logos/132/4072/thumbs/yfkihagcptzem3rhhf4h22343.gif", "Signal Induna Park" });
        }
    }
}
