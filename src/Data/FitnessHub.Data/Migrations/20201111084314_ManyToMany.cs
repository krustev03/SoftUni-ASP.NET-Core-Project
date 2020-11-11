using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_AspNetUsers_ApplicationUserId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Suplements_AspNetUsers_ApplicationUserId",
                table: "Suplements");

            migrationBuilder.DropIndex(
                name: "IX_Suplements_ApplicationUserId",
                table: "Suplements");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_ApplicationUserId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Suplements");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Equipments");

            migrationBuilder.CreateTable(
                name: "UserEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    EquipmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEquipments_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEquipments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSuplements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    SuplementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSuplements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSuplements_Suplements_SuplementId",
                        column: x => x.SuplementId,
                        principalTable: "Suplements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSuplements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEquipments_EquipmentId",
                table: "UserEquipments",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEquipments_UserId",
                table: "UserEquipments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSuplements_SuplementId",
                table: "UserSuplements",
                column: "SuplementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSuplements_UserId",
                table: "UserSuplements",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEquipments");

            migrationBuilder.DropTable(
                name: "UserSuplements");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Suplements",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Equipments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suplements_ApplicationUserId",
                table: "Suplements",
                column: "ApplicationUserId");

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
                name: "FK_Suplements_AspNetUsers_ApplicationUserId",
                table: "Suplements",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
