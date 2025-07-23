using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexecuter.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedForeignKeyForFirmware : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firmwares_Devices_DeviceId",
                table: "Firmwares");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgrammingLogs_Devices_DeviceId",
                table: "ProgrammingLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgrammingLogs_Firmwares_FirmwareId",
                table: "ProgrammingLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProgrammingLogs_FirmwareId",
                table: "ProgrammingLogs");

            migrationBuilder.DropColumn(
                name: "FirmwareId",
                table: "ProgrammingLogs");

            migrationBuilder.AddColumn<string>(
                name: "FirmwareFilePath",
                table: "ProgrammingLogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeviceId",
                table: "Firmwares",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Firmwares_Devices_DeviceId",
                table: "Firmwares",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgrammingLogs_Devices_DeviceId",
                table: "ProgrammingLogs",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firmwares_Devices_DeviceId",
                table: "Firmwares");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgrammingLogs_Devices_DeviceId",
                table: "ProgrammingLogs");

            migrationBuilder.DropColumn(
                name: "FirmwareFilePath",
                table: "ProgrammingLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "FirmwareId",
                table: "ProgrammingLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DeviceId",
                table: "Firmwares",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingLogs_FirmwareId",
                table: "ProgrammingLogs",
                column: "FirmwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Firmwares_Devices_DeviceId",
                table: "Firmwares",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgrammingLogs_Devices_DeviceId",
                table: "ProgrammingLogs",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgrammingLogs_Firmwares_FirmwareId",
                table: "ProgrammingLogs",
                column: "FirmwareId",
                principalTable: "Firmwares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
