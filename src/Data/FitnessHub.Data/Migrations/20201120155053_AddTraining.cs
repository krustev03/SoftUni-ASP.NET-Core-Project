using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessHub.Data.Migrations
{
    public partial class AddTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Trainings_TrainingId1",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_TrainingPrograms_TrainingProgramId1",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_TrainingProgramId1",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_TrainingId1",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId1",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "TrainingId1",
                table: "Exercises");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingProgramId",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TrainingId",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_TrainingProgramId",
                table: "Trainings",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TrainingId",
                table: "Exercises",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Trainings_TrainingId",
                table: "Exercises",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_TrainingPrograms_TrainingProgramId",
                table: "Trainings",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Trainings_TrainingId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_TrainingPrograms_TrainingProgramId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_TrainingProgramId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_TrainingId",
                table: "Exercises");

            migrationBuilder.AlterColumn<string>(
                name: "TrainingProgramId",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TrainingProgramId1",
                table: "Trainings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TrainingId",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TrainingId1",
                table: "Exercises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_TrainingProgramId1",
                table: "Trainings",
                column: "TrainingProgramId1");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TrainingId1",
                table: "Exercises",
                column: "TrainingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Trainings_TrainingId1",
                table: "Exercises",
                column: "TrainingId1",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_TrainingPrograms_TrainingProgramId1",
                table: "Trainings",
                column: "TrainingProgramId1",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
