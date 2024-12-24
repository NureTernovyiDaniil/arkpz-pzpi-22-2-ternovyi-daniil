using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChefMate_backend.Migrations
{
    /// <inheritdoc />
    public partial class FixMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_WorkZones_WorkZoneId1",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_WorkZoneId1",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "WorkZoneId1",
                table: "MenuItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkZoneId1",
                table: "MenuItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_WorkZoneId1",
                table: "MenuItems",
                column: "WorkZoneId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_WorkZones_WorkZoneId1",
                table: "MenuItems",
                column: "WorkZoneId1",
                principalTable: "WorkZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
