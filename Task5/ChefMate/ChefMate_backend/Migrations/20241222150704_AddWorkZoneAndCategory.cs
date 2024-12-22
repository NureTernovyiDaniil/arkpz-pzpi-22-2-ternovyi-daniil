using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChefMate_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkZoneAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkZoneId",
                table: "MenuItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WorkZoneId1",
                table: "MenuItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "WorkZones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkZones_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_WorkZoneId",
                table: "MenuItems",
                column: "WorkZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_WorkZoneId1",
                table: "MenuItems",
                column: "WorkZoneId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkZones_OrganizationId",
                table: "WorkZones",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_WorkZones_WorkZoneId",
                table: "MenuItems",
                column: "WorkZoneId",
                principalTable: "WorkZones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_WorkZones_WorkZoneId1",
                table: "MenuItems",
                column: "WorkZoneId1",
                principalTable: "WorkZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_WorkZones_WorkZoneId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_WorkZones_WorkZoneId1",
                table: "MenuItems");

            migrationBuilder.DropTable(
                name: "WorkZones");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_WorkZoneId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_WorkZoneId1",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "WorkZoneId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "WorkZoneId1",
                table: "MenuItems");
        }
    }
}
