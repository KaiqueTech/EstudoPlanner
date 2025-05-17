using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudoPlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchedulesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "tb_Schedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "tb_Schedules");
        }
    }
}
