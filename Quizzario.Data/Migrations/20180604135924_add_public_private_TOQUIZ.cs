using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizzario.Data.Migrations
{
    public partial class add_public_private_TOQUIZ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizAccessLevel",
                table: "Quiz",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizAccessLevel",
                table: "Quiz");
        }
    }
}
