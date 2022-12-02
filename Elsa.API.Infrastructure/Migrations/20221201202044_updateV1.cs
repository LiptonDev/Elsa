using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elsa.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "apiKeys",
                newName: "LastModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "apiKeys",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "apiKeys",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "apiKeys");

            migrationBuilder.RenameColumn(
                name: "LastModifiedOn",
                table: "apiKeys",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "apiKeys",
                newName: "CreateDate");
        }
    }
}
