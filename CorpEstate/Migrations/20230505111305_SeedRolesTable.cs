using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorpEstate.Migrations
{
    public partial class SeedRolesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Role_Id", "Role_Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Role_Id", "Role_Name" },
                values: new object[] { 2, "buyer" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Role_Id", "Role_Name" },
                values: new object[] { 3, "Seller" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Role_Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Role_Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Role_Id",
                keyValue: 3);
        }
    }
}
