﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Sale_Dance.Data.Migrations
{
    public partial class addBusinessIdToPB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "PublishedPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PublishedPosts_BusinessId",
                table: "PublishedPosts",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_PublishedPosts_Businesses_BusinessId",
                table: "PublishedPosts",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublishedPosts_Businesses_BusinessId",
                table: "PublishedPosts");

            migrationBuilder.DropIndex(
                name: "IX_PublishedPosts_BusinessId",
                table: "PublishedPosts");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "PublishedPosts");
        }
    }
}
