using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderFieldToCustomField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Filter_ShowOrder",
                table: "EntityCustomField",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Form_ShowOrder",
                table: "EntityCustomField",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Grid_ShowOrder",
                table: "EntityCustomField",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<bool>(
                name: "ShowInGrid",
                table: "EntityCustomField",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filter_ShowOrder",
                table: "EntityCustomField");

            migrationBuilder.DropColumn(
                name: "Form_ShowOrder",
                table: "EntityCustomField");

            migrationBuilder.DropColumn(
                name: "Grid_ShowOrder",
                table: "EntityCustomField");

            migrationBuilder.DropColumn(
                name: "ShowInGrid",
                table: "EntityCustomField");
        }
    }
}
