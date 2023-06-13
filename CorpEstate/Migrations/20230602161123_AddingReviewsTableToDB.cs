using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorpEstate.Migrations
{
    public partial class AddingReviewsTableToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyReviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Property_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyReviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_PropertyReviews_Properties_Property_Id",
                        column: x => x.Property_Id,
                        principalTable: "Properties",
                        principalColumn: "Property_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyReviews_Property_Id",
                table: "PropertyReviews",
                column: "Property_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyReviews");
        }
    }
}
