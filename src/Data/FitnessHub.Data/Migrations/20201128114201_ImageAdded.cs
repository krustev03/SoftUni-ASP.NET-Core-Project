using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class ImageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_SuplementId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Suplements",
                newName: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SuplementId",
                table: "Images",
                column: "SuplementId",
                unique: true,
                filter: "[SuplementId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_SuplementId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Suplements",
                newName: "ImageUrl");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SuplementId",
                table: "Images",
                column: "SuplementId");
        }
    }
}
