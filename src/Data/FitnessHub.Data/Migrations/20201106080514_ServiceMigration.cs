using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class ServiceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_AspNetUsers_SellerId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_SellerId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_SellerId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_SellerId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Equipments");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Equipments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_AuthorId",
                table: "Services",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_ApplicationUserId",
                table: "Equipments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_AspNetUsers_ApplicationUserId",
                table: "Equipments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_AuthorId",
                table: "Services",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_AspNetUsers_ApplicationUserId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_AuthorId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_AuthorId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_ApplicationUserId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Equipments");

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Equipments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_SellerId",
                table: "Services",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_SellerId",
                table: "Equipments",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_AspNetUsers_SellerId",
                table: "Equipments",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_SellerId",
                table: "Services",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
