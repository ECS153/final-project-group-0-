using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetapi.Migrations.Data
{
    public partial class AddCredentialsV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credential_Users_UserId",
                table: "Credential");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Credential",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Credential_UserId",
                table: "Credential",
                newName: "IX_Credential_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credential_Users_userId",
                table: "Credential",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credential_Users_userId",
                table: "Credential");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Credential",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Credential_userId",
                table: "Credential",
                newName: "IX_Credential_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credential_Users_UserId",
                table: "Credential",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
