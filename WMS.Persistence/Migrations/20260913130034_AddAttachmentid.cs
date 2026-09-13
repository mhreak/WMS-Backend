using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachmentid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_File_AttachmentFileId",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "AttachmentFileId",
                table: "Contract",
                newName: "Attachmentid");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_AttachmentFileId",
                table: "Contract",
                newName: "IX_Contract_Attachmentid");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_File_Attachmentid",
                table: "Contract",
                column: "Attachmentid",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_File_Attachmentid",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "Attachmentid",
                table: "Contract",
                newName: "AttachmentFileId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_Attachmentid",
                table: "Contract",
                newName: "IX_Contract_AttachmentFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_File_AttachmentFileId",
                table: "Contract",
                column: "AttachmentFileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
