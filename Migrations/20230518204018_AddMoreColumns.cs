using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compass.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Meeting_MeetingId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_MeetingId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "Person");

            migrationBuilder.AddColumn<string>(
                name: "Participants",
                table: "Meeting",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Participants",
                table: "Meeting");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Person",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeetingId",
                table: "Person",
                type: "INTEGER",
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
    }
}
