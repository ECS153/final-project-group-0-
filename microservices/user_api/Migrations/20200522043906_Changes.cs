using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetapi.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestSwaps",
                table: "RequestSwaps");

            migrationBuilder.RenameTable(
                name: "RequestSwaps",
                newName: "ProxySwaps");

            migrationBuilder.AlterColumn<string>(
                name: "RandToken",
                table: "ProxySwaps",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ip",
                table: "ProxySwaps",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "ProxySwaps",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Credential",
                table: "ProxySwaps",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                newName: "RequestSwaps");

            migrationBuilder.AlterColumn<string>(
                name: "RandToken",
                table: "RequestSwaps",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ip",
                table: "RequestSwaps",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "RequestSwaps",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Credential",
                table: "RequestSwaps",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestSwaps",
                table: "RequestSwaps",
                column: "Id");
        }
    }
}
