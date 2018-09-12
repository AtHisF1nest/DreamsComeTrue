using Microsoft.EntityFrameworkCore.Migrations;

namespace DreamsComeTrueAPI.Migrations
{
    public partial class Managementfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagementTypes_Photos_PhotoId",
                table: "ManagementTypes");

            migrationBuilder.DropIndex(
                name: "IX_ManagementTypes_PhotoId",
                table: "ManagementTypes");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "ManagementTypes");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "ManagementTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "ManagementTypes");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "ManagementTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManagementTypes_PhotoId",
                table: "ManagementTypes",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagementTypes_Photos_PhotoId",
                table: "ManagementTypes",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
