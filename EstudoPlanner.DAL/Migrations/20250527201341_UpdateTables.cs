using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudoPlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_StudyPlanDisciplines",
                table: "tb_StudyPlanDisciplines");

            migrationBuilder.AddColumn<Guid>(
                name: "IdStudyPlanDiscipline",
                table: "tb_StudyPlanDisciplines",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_StudyPlanDisciplines",
                table: "tb_StudyPlanDisciplines",
                column: "IdStudyPlanDiscipline");

            migrationBuilder.CreateIndex(
                name: "IX_tb_StudyPlanDisciplines_IdStudyPlan",
                table: "tb_StudyPlanDisciplines",
                column: "IdStudyPlan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_StudyPlanDisciplines",
                table: "tb_StudyPlanDisciplines");

            migrationBuilder.DropIndex(
                name: "IX_tb_StudyPlanDisciplines_IdStudyPlan",
                table: "tb_StudyPlanDisciplines");

            migrationBuilder.DropColumn(
                name: "IdStudyPlanDiscipline",
                table: "tb_StudyPlanDisciplines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_StudyPlanDisciplines",
                table: "tb_StudyPlanDisciplines",
                columns: new[] { "IdStudyPlan", "Discipline" });
        }
    }
}
