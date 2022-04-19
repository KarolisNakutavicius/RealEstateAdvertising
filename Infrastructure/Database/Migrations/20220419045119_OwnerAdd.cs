using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    public partial class OwnerAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Advertisments");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Advertisments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisments_OwnerId",
                table: "Advertisments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisments_AspNetUsers_OwnerId",
                table: "Advertisments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisments_AspNetUsers_OwnerId",
                table: "Advertisments");

            migrationBuilder.DropIndex(
                name: "IX_Advertisments_OwnerId",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Advertisments");

            migrationBuilder.AddColumn<int>(
                name: "OwnerUserId",
                table: "Advertisments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
