using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG_POE_CMCS.Migrations
{
    /// <inheritdoc />
    public partial class DocsMoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Claim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Claim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Claim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Claim",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Claim");
        }
    }
}
