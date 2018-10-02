using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DreamsComeTrueAPI.Migrations
{
    public partial class invitations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInvitations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserInvitatingId = table.Column<int>(nullable: true),
                    InvitedUserId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    InvitationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInvitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInvitations_Users_InvitedUserId",
                        column: x => x.InvitedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInvitations_Users_UserInvitatingId",
                        column: x => x.UserInvitatingId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInvitations_InvitedUserId",
                table: "UserInvitations",
                column: "InvitedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInvitations_UserInvitatingId",
                table: "UserInvitations",
                column: "UserInvitatingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInvitations");
        }
    }
}
