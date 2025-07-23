using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexecuter.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMigrationForProgrammingcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirmwareFilePath",
                table: "ProgrammingLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirmwareFilePath",
                table: "ProgrammingLogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
