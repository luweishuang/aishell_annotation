using Microsoft.EntityFrameworkCore.Migrations;

namespace AIShellAn.Server.Migrations
{
    public partial class _20200403 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastLoginIP",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginIP",
                table: "User");
        }
    }
}
