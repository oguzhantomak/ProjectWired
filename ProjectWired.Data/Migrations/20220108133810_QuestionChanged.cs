using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectWired.Data.Migrations
{
    public partial class QuestionChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorrectChoiceKey",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectChoiceKey",
                table: "Questions");
        }
    }
}
