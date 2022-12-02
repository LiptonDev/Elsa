using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elsa.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Fails",
                table: "mails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Sended",
                table: "mails",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fails",
                table: "mails");

            migrationBuilder.DropColumn(
                name: "Sended",
                table: "mails");
        }
    }
}
