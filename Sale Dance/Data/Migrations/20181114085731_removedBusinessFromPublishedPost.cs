using Microsoft.EntityFrameworkCore.Migrations;

namespace Sale_Dance.Data.Migrations
{
    public partial class removedBusinessFromPublishedPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublishedPosts_Businesses_Businessid",
                table: "PublishedPosts");

            migrationBuilder.DropIndex(
                name: "IX_PublishedPosts_Businessid",
                table: "PublishedPosts");

            migrationBuilder.DropColumn(
                name: "Businessid",
                table: "PublishedPosts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Businessid",
                table: "PublishedPosts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublishedPosts_Businessid",
                table: "PublishedPosts",
                column: "Businessid");

            migrationBuilder.AddForeignKey(
                name: "FK_PublishedPosts_Businesses_Businessid",
                table: "PublishedPosts",
                column: "Businessid",
                principalTable: "Businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
