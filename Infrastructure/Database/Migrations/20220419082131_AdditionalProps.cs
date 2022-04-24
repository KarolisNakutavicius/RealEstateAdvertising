using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    public partial class AdditionalProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisments_AspNetUsers_OwnerId",
                table: "Advertisments");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisments_Buildings_BuildingId",
                table: "Advertisments");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_BuildingCategories_CategoryId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_CategoryId",
                table: "Buildings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisments",
                table: "Advertisments");

            migrationBuilder.RenameTable(
                name: "Advertisments",
                newName: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Buildings",
                newName: "Size");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisments_OwnerId",
                table: "Advertisements",
                newName: "IX_Advertisements_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisments_BuildingId",
                table: "Advertisements",
                newName: "IX_Advertisements_BuildingId");

            migrationBuilder.AlterColumn<int>(
                name: "Address_Number",
                table: "Buildings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsRent",
                table: "Advertisements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Advertisements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_OwnerId",
                table: "Advertisements",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Buildings_BuildingId",
                table: "Advertisements",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_OwnerId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Buildings_BuildingId",
                table: "Advertisements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "IsRent",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Advertisements");

            migrationBuilder.RenameTable(
                name: "Advertisements",
                newName: "Advertisments");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Buildings",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_OwnerId",
                table: "Advertisments",
                newName: "IX_Advertisments_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_BuildingId",
                table: "Advertisments",
                newName: "IX_Advertisments_BuildingId");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Number",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisments",
                table: "Advertisments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CategoryId",
                table: "Buildings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisments_AspNetUsers_OwnerId",
                table: "Advertisments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisments_Buildings_BuildingId",
                table: "Advertisments",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_BuildingCategories_CategoryId",
                table: "Buildings",
                column: "CategoryId",
                principalTable: "BuildingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
