using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class ImageImpr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Equipments",
                newName: "ImageId");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: true),
                    SuplementId = table.Column<int>(type: "int", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemoteImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_AspNetUsers_AddedByUserId",
                        column: x => x.AddedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_Suplements_SuplementId",
                        column: x => x.SuplementId,
                        principalTable: "Suplements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AddedByUserId",
                table: "Images",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_EquipmentId",
                table: "Images",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SuplementId",
                table: "Images",
                column: "SuplementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Equipments",
                newName: "ImageUrl");
        }
    }
}
