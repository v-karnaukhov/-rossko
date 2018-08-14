using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PermutationsService.Web.Migrations
{
    public partial class AddResultCountAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Result",
                table: "PermutationEntries",
                newName: "ResultString");

            migrationBuilder.AddColumn<long>(
                name: "ResultCount",
                table: "PermutationEntries",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultCount",
                table: "PermutationEntries");

            migrationBuilder.RenameColumn(
                name: "ResultString",
                table: "PermutationEntries",
                newName: "Result");
        }
    }
}
