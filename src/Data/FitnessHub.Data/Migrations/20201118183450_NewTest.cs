using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class NewTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalPassword",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
