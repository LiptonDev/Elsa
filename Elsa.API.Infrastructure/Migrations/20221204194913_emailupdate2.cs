using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elsa.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class emailupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MailboxBusy",
                table: "mails",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailboxBusy",
                table: "mails");
        }
    }
}
