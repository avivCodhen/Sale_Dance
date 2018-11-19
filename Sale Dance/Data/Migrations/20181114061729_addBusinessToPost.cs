using Microsoft.EntityFrameworkCore.Migrations;

namespace Sale_Dance.Data.Migrations
{
    public partial class addBusinessToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BusinessId",
                table: "Posts",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Businesses_BusinessId",
                table: "Posts",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Businesses_BusinessId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BusinessId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Posts");
        }
    }
}
