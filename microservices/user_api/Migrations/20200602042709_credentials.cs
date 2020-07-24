using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetapi.Migrations
{
    public partial class credentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credential_Users_UserId",
                table: "Credential");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Credential",
                table: "Credential");

            migrationBuilder.DropIndex(
                name: "IX_Credential_UserId",
                table: "Credential");

            migrationBuilder.RenameTable(
                name: "Credential",
                newName: "Credentials");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Credentials",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Credentials",
                table: "Credentials",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Credentials",
                table: "Credentials");

            migrationBuilder.RenameTable(
                name: "Credentials",
                newName: "Credential");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Credential",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Credential",
                table: "Credential",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Credential_UserId",
                table: "Credential",
                column: "UserId");

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
