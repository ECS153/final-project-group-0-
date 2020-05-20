using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetapi.Migrations.RequestSwap
{
    public partial class Renaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestSwaps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    FieldId = table.Column<int>(nullable: false),
                    Ip = table.Column<string>(nullable: false),
                    Domain = table.Column<string>(nullable: false),
                    RandToken = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestSwaps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestSwaps");
        }
    }
}
