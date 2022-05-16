using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    public partial class AddCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Buildings",
                newName: "Size_PlotSize");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Buildings",
                newName: "Size_MeasurementUnit");

            migrationBuilder.AddColumn<int>(
                name: "Address_CityId",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size_BuildingSize",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Advertisements",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_Address_CityId",
                table: "Buildings",
                column: "Address_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Cities_Address_CityId",
                table: "Buildings",
                column: "Address_CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Cities_Address_CityId",
                table: "Buildings");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_Address_CityId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Address_CityId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Size_BuildingSize",
                table: "Buildings");

            migrationBuilder.RenameColumn(
                name: "Size_PlotSize",
                table: "Buildings",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "Size_MeasurementUnit",
                table: "Buildings",
                newName: "Address_City");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Advertisements",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
