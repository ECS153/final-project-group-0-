using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetapi.Migrations.Data
{
    public partial class NullableType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credential_Users_userId",
                table: "Credential");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Credential",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "domain",
                table: "Credential",
                newName: "Domain");

            migrationBuilder.RenameIndex(
                name: "IX_Credential_userId",
                table: "Credential",
                newName: "IX_Credential_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Credential",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Credential_Users_UserId",
                table: "Credential",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credential_Users_UserId",
                table: "Credential");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Credential",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Domain",
                table: "Credential",
                newName: "domain");

            migrationBuilder.RenameIndex(
                name: "IX_Credential_UserId",
                table: "Credential",
                newName: "IX_Credential_userId");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Credential",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Credential_Users_userId",
                table: "Credential",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
