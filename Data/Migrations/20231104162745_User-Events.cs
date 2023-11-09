using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agendex.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Identity",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                schema: "Identity",
                table: "Events",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_UserId",
                schema: "Identity",
                table: "Events",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_UserId",
                schema: "Identity",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UserId",
                schema: "Identity",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Identity",
                table: "Events");
        }
    }
}
