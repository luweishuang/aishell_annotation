using Microsoft.EntityFrameworkCore.Migrations;

namespace AIShellAn.Server.Migrations
{
    public partial class _202004031 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Effective",
                table: "ShortSpeechItem");

            migrationBuilder.DropColumn(
                name: "Effective",
                table: "LongSpeechItem");

            migrationBuilder.AddColumn<float>(
                name: "Duration",
                table: "ShortSpeechItem",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Duration",
                table: "LongSpeechItem",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "Effective",
                table: "AnnotationResult",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "ShortSpeechItem");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "LongSpeechItem");

            migrationBuilder.DropColumn(
                name: "Effective",
                table: "AnnotationResult");

            migrationBuilder.AddColumn<bool>(
                name: "Effective",
                table: "ShortSpeechItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Effective",
                table: "LongSpeechItem",
                nullable: true);
        }
    }
}
