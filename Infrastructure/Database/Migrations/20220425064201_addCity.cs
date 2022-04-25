using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    public partial class addCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Buildings");

            migrationBuilder.AddColumn<int>(
                name: "Address_CityId",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
