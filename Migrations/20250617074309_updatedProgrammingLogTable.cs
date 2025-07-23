using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexecuter.Migrations
{
    /// <inheritdoc />
    public partial class updatedProgrammingLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogOutput",
                table: "ProgrammingLogs",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogOutput",
                table: "ProgrammingLogs");
        }
    }
}
