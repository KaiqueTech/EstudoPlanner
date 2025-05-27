using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudoPlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InsertTableDiscipline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_StudyPlanDisciplines",
                columns: table => new
                {
                    IdStudyPlan = table.Column<Guid>(type: "TEXT", nullable: false),
                    Discipline = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_StudyPlanDisciplines", x => new { x.IdStudyPlan, x.Discipline });
                    table.ForeignKey(
                        name: "FK_tb_StudyPlanDisciplines_tb_StudyPlans_IdStudyPlan",
                        column: x => x.IdStudyPlan,
                        principalTable: "tb_StudyPlans",
                        principalColumn: "IdStudyPlan",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_StudyPlanDisciplines");
        }
    }
}
