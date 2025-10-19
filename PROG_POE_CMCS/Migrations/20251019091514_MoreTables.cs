using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG_POE_CMCS.Migrations
{
    /// <inheritdoc />
    public partial class MoreTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Claim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Claim");
        }
    }
}
