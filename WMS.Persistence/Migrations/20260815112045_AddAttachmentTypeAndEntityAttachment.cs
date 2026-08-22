using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachmentTypeAndEntityAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityAttachment_File_FileAssetId",
                table: "EntityAttachment");

            migrationBuilder.DropIndex(
                name: "IX_EntityAttachment_EntityId_EntityType",
                table: "EntityAttachment");

            migrationBuilder.DropIndex(
                name: "IX_EntityAttachment_EntityType",
                table: "EntityAttachment");

            migrationBuilder.DropIndex(
                name: "IX_EntityAttachment_FileAssetId_EntityId_EntityType",
                table: "EntityAttachment");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "EntityAttachment");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EntityAttachment");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "EntityAttachment");

            migrationBuilder.RenameColumn(
                name: "FileAssetId",
                table: "EntityAttachment",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachment_FileAssetId",
                table: "EntityAttachment",
                newName: "IX_EntityAttachment_UserId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EntityAttachment",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentTypeId",
                table: "EntityAttachment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "EntityAttachment",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "EntityAttachment",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "EntityAttachment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AttachmentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachment_AttachmentTypeId",
                table: "EntityAttachment",
                column: "AttachmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachment_EntityId_AttachmentTypeId",
                table: "EntityAttachment",
                columns: new[] { "EntityId", "AttachmentTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachment_IsDeleted",
                table: "EntityAttachment",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentType_EntityType",
                table: "AttachmentType",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentType_EntityType_Title",
                table: "AttachmentType",
                columns: new[] { "EntityType", "Title" });

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentType_IsActive",
                table: "AttachmentType",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentType_IsDeleted",
                table: "AttachmentType",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityAttachment_AttachmentType_AttachmentTypeId",
                table: "EntityAttachment",
                column: "AttachmentTypeId",
                principalTable: "AttachmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityAttachment_AttachmentType_AttachmentTypeId",
                table: "EntityAttachment");

            migrationBuilder.DropTable(
                name: "AttachmentType");

            migrationBuilder.DropIndex(
                name: "IX_EntityAttachment_AttachmentTypeId",
                table: "EntityAttachment");

            migrationBuilder.DropIndex(
                name: "IX_EntityAttachment_EntityId_AttachmentTypeId",
                table: "EntityAttachment");

            migrationBuilder.DropIndex(
                name: "IX_EntityAttachment_IsDeleted",
                table: "EntityAttachment");

            migrationBuilder.DropColumn(
                name: "AttachmentTypeId",
                table: "EntityAttachment");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "EntityAttachment");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "EntityAttachment");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "EntityAttachment");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EntityAttachment",
                newName: "FileAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachment_UserId",
                table: "EntityAttachment",
                newName: "IX_EntityAttachment_FileAssetId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EntityAttachment",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EntityType",
                table: "EntityAttachment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EntityAttachment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "EntityAttachment",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachment_EntityId_EntityType",
                table: "EntityAttachment",
                columns: new[] { "EntityId", "EntityType" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachment_EntityType",
                table: "EntityAttachment",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachment_FileAssetId_EntityId_EntityType",
                table: "EntityAttachment",
                columns: new[] { "FileAssetId", "EntityId", "EntityType" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityAttachment_File_FileAssetId",
                table: "EntityAttachment",
                column: "FileAssetId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
