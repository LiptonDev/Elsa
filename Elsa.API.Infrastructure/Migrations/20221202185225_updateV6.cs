using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elsa.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateV6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sended",
                table: "mails");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "apiKeys");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "apiKeys");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "apiKeys");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "apiKeys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Sended",
                table: "mails",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "apiKeys",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "apiKeys",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "apiKeys",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "apiKeys",
                type: "TEXT",
                nullable: true);
        }
    }
}
