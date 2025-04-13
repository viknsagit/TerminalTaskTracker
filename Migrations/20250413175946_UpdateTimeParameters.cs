using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerminalTaskTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTimeParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreationTime",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Tasks");
        }
    }
}
