using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo_A_P_I.Migrations
{
    /// <inheritdoc />
    public partial class Added_TodoDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TodoItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "TodoItems");
        }
    }
}
