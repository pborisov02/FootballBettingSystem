using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBettingSystem.Web.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Leagues",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Country", "LogoUrl", "Name" },
                values: new object[] { 1, "Spain", "https://content.sportslogos.net/leagues/thumbs/130.gif", "La Liga" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Country", "LogoUrl", "Name" },
                values: new object[] { 2, "Germany", "https://content.sportslogos.net/leagues/thumbs/132.gif", "Bundesliga" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "BadgeUrl", "LeagueId", "Name", "StadiumName" },
                values: new object[,]
                {
                    { 1, "https://content.sportslogos.net/logos/130/4016/thumbs/hy5fvvdkee83gg3r5ym22zr5o.gif", 1, "Barcelona", "Camp Nou" },
                    { 2, "https://content.sportslogos.net/logos/130/4017/thumbs/yfhezt5oyr0jbq29u4hp50w63.gif", 1, "Real Madrid", "Santiago Bernabeu" },
                    { 3, "https://content.sportslogos.net/logos/132/4069/thumbs/rr72mhpas38h85jdw85neas5f.gif", 2, "Bayern Munich", "Allianz Arena" },
                    { 4, "https://content.sportslogos.net/logos/132/4072/thumbs/yfkihagcptzem3rhhf4h22343.gif", 2, "Borussia Dortmund", "Signal Induna Park" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "Appearance", "FirstName", "GameId", "Goals", "KitNumber", "LastName", "Position", "TeamId" },
                values: new object[] { 1, 18, 0, "Pablo", null, 0, 30, "Gavi", 8, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "Appearance", "FirstName", "GameId", "Goals", "KitNumber", "LastName", "Position", "TeamId" },
                values: new object[] { 2, 22, 0, "Vinicius", null, 0, 22, "Jr", 11, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Leagues");
        }
    }
}
