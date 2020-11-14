using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class NewsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
