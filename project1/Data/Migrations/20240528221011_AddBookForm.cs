using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishDte",
                table: "Books",
                newName: "PublishDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Books",
                newName: "PublishDte");
        }
    }
}
