using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFileIdToContractAndFixStatementFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "ContractorStatement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentFileId",
                table: "Contract",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "Contract",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatement_FileId",
                table: "ContractorStatement",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_AttachmentFileId",
                table: "Contract",
                column: "AttachmentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_FileId",
                table: "Contract",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_File_AttachmentFileId",
                table: "Contract",
                column: "AttachmentFileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_File_FileId",
                table: "Contract",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatement_File_FileId",
                table: "ContractorStatement",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_File_AttachmentFileId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_File_FileId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatement_File_FileId",
                table: "ContractorStatement");

            migrationBuilder.DropIndex(
                name: "IX_ContractorStatement_FileId",
                table: "ContractorStatement");

            migrationBuilder.DropIndex(
                name: "IX_Contract_AttachmentFileId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_FileId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "ContractorStatement");

            migrationBuilder.DropColumn(
                name: "AttachmentFileId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Contract");
        }
    }
}
