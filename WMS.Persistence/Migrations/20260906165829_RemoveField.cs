using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "ContractorStatement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatement_FileId",
                table: "ContractorStatement",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatement_EntityAttachment_FileId",
                table: "ContractorStatement",
                column: "FileId",
                principalTable: "EntityAttachment",
                principalColumn: "Id");
        }
    }
}
