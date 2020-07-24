using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetapi.Migrations
{
    public partial class requestSwap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestSwap_Users_UserId",
                table: "RequestSwap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestSwap",
                table: "RequestSwap");

            migrationBuilder.DropIndex(
                name: "IX_RequestSwap_UserId",
                table: "RequestSwap");

            migrationBuilder.RenameTable(
                name: "RequestSwap",
                newName: "RequestSwaps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestSwaps",
                table: "RequestSwaps",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestSwaps",
                table: "RequestSwaps");

            migrationBuilder.RenameTable(
                name: "RequestSwaps",
                newName: "RequestSwap");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestSwap",
                table: "RequestSwap",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestSwap_UserId",
                table: "RequestSwap",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestSwap_Users_UserId",
                table: "RequestSwap",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
