using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorpEstate.Migrations
{
    public partial class UpdatePropertyTableAndDTOs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Property_UpdatedTime",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Property_UpdatedTime",
                table: "Properties");
        }
    }
}
