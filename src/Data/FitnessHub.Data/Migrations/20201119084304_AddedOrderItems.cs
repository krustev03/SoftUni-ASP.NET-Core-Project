using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class AddedOrderItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Suplements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Equipments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suplements_OrderId",
                table: "Suplements",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_OrderId",
                table: "Equipments",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Orders_OrderId",
                table: "Equipments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suplements_Orders_OrderId",
                table: "Suplements",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Orders_OrderId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Suplements_Orders_OrderId",
                table: "Suplements");

            migrationBuilder.DropIndex(
                name: "IX_Suplements_OrderId",
                table: "Suplements");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_OrderId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Suplements");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Equipments");
        }
    }
}
