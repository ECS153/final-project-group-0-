using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetapi.Migrations
{
    public partial class RenamingProxyTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestReplacements",
                table: "RequestReplacements");

            migrationBuilder.RenameTable(
                name: "RequestReplacements",
                newName: "ProxySwaps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProxySwaps",
                table: "ProxySwaps",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProxySwaps",
                table: "ProxySwaps");

            migrationBuilder.RenameTable(
                name: "ProxySwaps",
                newName: "RequestReplacements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestReplacements",
                table: "RequestReplacements",
                column: "Id");
        }
    }
}
