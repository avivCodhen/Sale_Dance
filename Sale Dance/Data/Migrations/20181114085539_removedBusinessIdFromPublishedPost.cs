using Microsoft.EntityFrameworkCore.Migrations;

namespace Sale_Dance.Data.Migrations
{
    public partial class removedBusinessIdFromPublishedPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublishedPosts_Businesses_BusinessId",
                table: "PublishedPosts");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "PublishedPosts",
                newName: "Businessid");

            migrationBuilder.RenameIndex(
                name: "IX_PublishedPosts_BusinessId",
                table: "PublishedPosts",
                newName: "IX_PublishedPosts_Businessid");

            migrationBuilder.AlterColumn<int>(
                name: "Businessid",
                table: "PublishedPosts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PublishedPosts_Businesses_Businessid",
                table: "PublishedPosts",
                column: "Businessid",
                principalTable: "Businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublishedPosts_Businesses_Businessid",
                table: "PublishedPosts");

            migrationBuilder.RenameColumn(
                name: "Businessid",
                table: "PublishedPosts",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_PublishedPosts_Businessid",
                table: "PublishedPosts",
                newName: "IX_PublishedPosts_BusinessId");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "PublishedPosts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PublishedPosts_Businesses_BusinessId",
                table: "PublishedPosts",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
