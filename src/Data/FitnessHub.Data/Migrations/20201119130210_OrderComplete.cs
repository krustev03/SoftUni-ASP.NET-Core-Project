using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class OrderComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "OrderEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderEquipments_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderEquipments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderSuplements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    SuplementId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSuplements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSuplements_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSuplements_Suplements_SuplementId",
                        column: x => x.SuplementId,
                        principalTable: "Suplements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderEquipments_EquipmentId",
                table: "OrderEquipments",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEquipments_OrderId",
                table: "OrderEquipments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuplements_OrderId",
                table: "OrderSuplements",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuplements_SuplementId",
                table: "OrderSuplements",
                column: "SuplementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderEquipments");

            migrationBuilder.DropTable(
                name: "OrderSuplements");

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
    }
}
