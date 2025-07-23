using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexecuter.Migrations
{
    /// <inheritdoc />
    public partial class theLastMigrationModifiedForProgrammingLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogOutput",
                table: "ProgrammingLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogOutput",
                table: "ProgrammingLogs",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);
        }
    }
}
