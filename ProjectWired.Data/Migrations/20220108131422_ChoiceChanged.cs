using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectWired.Data.Migrations
{
    public partial class ChoiceChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Choices",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Choices");
        }
    }
}
