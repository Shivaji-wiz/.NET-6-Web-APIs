using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "VillasNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 16, 11, 20, 42, 998, DateTimeKind.Local).AddTicks(5937));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 16, 11, 20, 42, 998, DateTimeKind.Local).AddTicks(5954));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 16, 11, 20, 42, 998, DateTimeKind.Local).AddTicks(5955));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 16, 11, 20, 42, 998, DateTimeKind.Local).AddTicks(5956));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 16, 11, 20, 42, 998, DateTimeKind.Local).AddTicks(5957));

            migrationBuilder.CreateIndex(
                name: "IX_VillasNumbers_VillaId",
                table: "VillasNumbers",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillasNumbers_Villas_VillaId",
                table: "VillasNumbers",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            //this cascade means if villa is deleted then it will also delete the corresponding row;
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillasNumbers_Villas_VillaId",
                table: "VillasNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillasNumbers_VillaId",
                table: "VillasNumbers");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "VillasNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 16, 9, 51, 48, 541, DateTimeKind.Local).AddTicks(6685));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 16, 9, 51, 48, 541, DateTimeKind.Local).AddTicks(6698));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 16, 9, 51, 48, 541, DateTimeKind.Local).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 16, 9, 51, 48, 541, DateTimeKind.Local).AddTicks(6702));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 16, 9, 51, 48, 541, DateTimeKind.Local).AddTicks(6704));
        }
    }
}
