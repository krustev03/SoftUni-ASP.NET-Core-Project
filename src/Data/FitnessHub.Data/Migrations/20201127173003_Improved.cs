using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class Improved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserSuplements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserSuplements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserSuplements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserSuplements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserEquipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserEquipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserEquipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserEquipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "OrderSuplements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "OrderSuplements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderSuplements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "OrderSuplements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "OrderEquipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "OrderEquipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderEquipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "OrderEquipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSuplements_IsDeleted",
                table: "UserSuplements",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserEquipments_IsDeleted",
                table: "UserEquipments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuplements_IsDeleted",
                table: "OrderSuplements",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEquipments_IsDeleted",
                table: "OrderEquipments",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSuplements_IsDeleted",
                table: "UserSuplements");

            migrationBuilder.DropIndex(
                name: "IX_UserEquipments_IsDeleted",
                table: "UserEquipments");

            migrationBuilder.DropIndex(
                name: "IX_OrderSuplements_IsDeleted",
                table: "OrderSuplements");

            migrationBuilder.DropIndex(
                name: "IX_OrderEquipments_IsDeleted",
                table: "OrderEquipments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserSuplements");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserSuplements");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserSuplements");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserSuplements");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserEquipments");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserEquipments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserEquipments");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserEquipments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OrderSuplements");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "OrderSuplements");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderSuplements");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "OrderSuplements");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OrderEquipments");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "OrderEquipments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderEquipments");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "OrderEquipments");
        }
    }
}
