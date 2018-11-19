using Microsoft.EntityFrameworkCore.Migrations;

namespace Sale_Dance.Data.Migrations
{
    public partial class updateBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessOwnerId",
                table: "Businesses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessOwnerId",
                table: "Businesses",
                column: "BusinessOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_AspNetUsers_BusinessOwnerId",
                table: "Businesses",
                column: "BusinessOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_AspNetUsers_BusinessOwnerId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinessOwnerId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "BusinessOwnerId",
                table: "Businesses");
        }
    }
}
