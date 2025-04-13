using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerminalTaskTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddIsClosedParam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Projects");
        }
    }
}
