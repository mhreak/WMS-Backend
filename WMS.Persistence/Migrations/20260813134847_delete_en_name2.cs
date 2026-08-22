using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class delete_en_name2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "ContractCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "ContractCategory",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }
    }
}
