using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compass.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeetingId",
                table: "Person",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetingDate",
                table: "Meeting",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Meeting",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_MeetingId",
                table: "Person",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Meeting_MeetingId",
                table: "Person",
                column: "MeetingId",
                principalTable: "Meeting",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Meeting_MeetingId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_MeetingId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "MeetingDate",
                table: "Meeting");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Meeting");
        }
    }
}
