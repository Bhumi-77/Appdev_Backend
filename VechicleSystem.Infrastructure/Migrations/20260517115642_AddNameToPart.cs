using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VechicleSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Parts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Parts");
        }
    }
}
