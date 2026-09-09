using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityCustomFieldTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityCustomField",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FieldType = table.Column<int>(type: "int", nullable: false),
                    Config = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityCustomField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityCustomField_Entity",
                columns: table => new
                {
                    EntityCustomFieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityCustomField_Entity", x => new { x.EntityCustomFieldId, x.EntityId });
                    table.ForeignKey(
                        name: "FK_EntityCustomField_Entity_EntityCustomField_EntityCustomFieldId",
                        column: x => x.EntityCustomFieldId,
                        principalTable: "EntityCustomField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomField_EntityType",
                table: "EntityCustomField",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomField_EntityType_FieldName",
                table: "EntityCustomField",
                columns: new[] { "EntityType", "FieldName" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomField_IsActive",
                table: "EntityCustomField",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomField_IsDeleted",
                table: "EntityCustomField",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomField_Entity_EntityCustomFieldId",
                table: "EntityCustomField_Entity",
                column: "EntityCustomFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomField_Entity_EntityId",
                table: "EntityCustomField_Entity",
                column: "EntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityCustomField_Entity");

            migrationBuilder.DropTable(
                name: "EntityCustomField");
        }
    }
}
