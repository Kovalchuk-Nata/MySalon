using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySalonWeb.Migrations
{
    /// <inheritdoc />
    public partial class editModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "Surename",
                table: "Experts",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Surename",
                table: "Clients",
                newName: "Surname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Experts",
                newName: "Surename");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Clients",
                newName: "Surename");

            migrationBuilder.AddColumn<DateTime>(
                name: "Period",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
