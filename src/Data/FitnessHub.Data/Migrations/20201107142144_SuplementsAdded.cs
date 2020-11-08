using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class SuplementsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Suplements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suplements_ApplicationUserId",
                table: "Suplements",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suplements_AspNetUsers_ApplicationUserId",
                table: "Suplements",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suplements_AspNetUsers_ApplicationUserId",
                table: "Suplements");

            migrationBuilder.DropIndex(
                name: "IX_Suplements_ApplicationUserId",
                table: "Suplements");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Suplements");
        }
    }
}
