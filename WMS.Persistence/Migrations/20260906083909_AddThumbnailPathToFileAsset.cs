using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailPathToFileAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailPath",
                table: "File",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailPath",
                table: "File");
        }
    }
}
