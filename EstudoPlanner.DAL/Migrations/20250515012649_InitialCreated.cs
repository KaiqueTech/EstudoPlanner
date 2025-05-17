using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudoPlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_Users",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "tb_StudyPlans",
                columns: table => new
                {
                    IdStudyPlan = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IdUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_StudyPlans", x => x.IdStudyPlan);
                    table.ForeignKey(
                        name: "FK_tb_StudyPlans_tb_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "tb_Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Schedules",
                columns: table => new
                {
                    IdSchedule = table.Column<Guid>(type: "TEXT", nullable: false),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    IdStudyPlan = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Schedules", x => x.IdSchedule);
                    table.ForeignKey(
                        name: "FK_tb_Schedules_tb_StudyPlans_IdStudyPlan",
                        column: x => x.IdStudyPlan,
                        principalTable: "tb_StudyPlans",
                        principalColumn: "IdStudyPlan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_Schedules_IdStudyPlan",
                table: "tb_Schedules",
                column: "IdStudyPlan");

            migrationBuilder.CreateIndex(
                name: "IX_tb_StudyPlans_IdUser",
                table: "tb_StudyPlans",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Users_Email",
                table: "tb_Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Schedules");

            migrationBuilder.DropTable(
                name: "tb_StudyPlans");

            migrationBuilder.DropTable(
                name: "tb_Users");
        }
    }
}
