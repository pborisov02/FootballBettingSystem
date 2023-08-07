using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBettingSystem.Web.Migrations
{
    public partial class ChangeWalletBallanceToWalletBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalletBallance",
                table: "AspNetUsers",
                newName: "WalletBalance");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 7, 22, 0, 36, 569, DateTimeKind.Utc).AddTicks(5070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 5, 10, 23, 44, 40, DateTimeKind.Utc).AddTicks(5024));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalletBalance",
                table: "AspNetUsers",
                newName: "WalletBallance");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 5, 10, 23, 44, 40, DateTimeKind.Utc).AddTicks(5024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 7, 22, 0, 36, 569, DateTimeKind.Utc).AddTicks(5070));
        }
    }
}
