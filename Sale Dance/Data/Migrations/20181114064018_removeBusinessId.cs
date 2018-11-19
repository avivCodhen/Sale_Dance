using Microsoft.EntityFrameworkCore.Migrations;

namespace Sale_Dance.Data.Migrations
{
    public partial class removeBusinessId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Businesses_BusinessId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Posts",
                newName: "Businessid");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_BusinessId",
                table: "Posts",
                newName: "IX_Posts_Businessid");

            migrationBuilder.AlterColumn<int>(
                name: "Businessid",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Businesses_Businessid",
                table: "Posts",
                column: "Businessid",
                principalTable: "Businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Businesses_Businessid",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Businessid",
                table: "Posts",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_Businessid",
                table: "Posts",
                newName: "IX_Posts_BusinessId");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Businesses_BusinessId",
                table: "Posts",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
