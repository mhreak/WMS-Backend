using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFileIdFromContractorStatement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatement_EntityAttachment_FileId",
                table: "ContractorStatement");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatement_EntityAttachment_FileId",
                table: "ContractorStatement",
                column: "FileId",
                principalTable: "EntityAttachment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatement_EntityAttachment_FileId",
                table: "ContractorStatement");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatement_EntityAttachment_FileId",
                table: "ContractorStatement",
                column: "FileId",
                principalTable: "EntityAttachment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
