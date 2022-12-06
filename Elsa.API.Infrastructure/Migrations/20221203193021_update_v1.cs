using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elsa.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "roles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
