using Microsoft.EntityFrameworkCore.Migrations;

namespace Sale_Dance.Data.Migrations
{
    public partial class addSuperAdminToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnderId",
                table: "Sales",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "isSuperAdmin",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSuperAdmin",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "OwnderId",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
