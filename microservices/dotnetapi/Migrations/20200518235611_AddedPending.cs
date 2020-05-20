using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetapi.Migrations
{
    public partial class AddedPending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestReplacements",
                table: "RequestReplacements");

            migrationBuilder.RenameTable(
                name: "RequestReplacements",
                newName: "RequestReplacement");

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "RequestReplacement",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestReplacement",
                table: "RequestReplacement",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestReplacement",
                table: "RequestReplacement");

            migrationBuilder.DropColumn(
                name: "Domain",
                table: "RequestReplacement");

            migrationBuilder.RenameTable(
                name: "RequestReplacement",
                newName: "RequestReplacements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestReplacements",
                table: "RequestReplacements",
                column: "Id");
        }
    }
}
