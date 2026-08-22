using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeContractorStatementFileIdToEntityAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ContractorStatement");

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "ContractorStatement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatement_FileId",
                table: "ContractorStatement",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatement_EntityAttachment_FileId",
                table: "ContractorStatement",
                column: "FileId",
                principalTable: "EntityAttachment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatement_EntityAttachment_FileId",
                table: "ContractorStatement");

            migrationBuilder.DropIndex(
                name: "IX_ContractorStatement_FileId",
                table: "ContractorStatement");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "ContractorStatement");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ContractorStatement",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
