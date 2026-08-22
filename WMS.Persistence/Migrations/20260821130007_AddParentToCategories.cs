using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddParentToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "ContractorCategory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "ContractCategory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractorCategory_ParentId",
                table: "ContractorCategory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCategory_ParentId",
                table: "ContractCategory",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCategory_ContractCategory_ParentId",
                table: "ContractCategory",
                column: "ParentId",
                principalTable: "ContractCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorCategory_ContractorCategory_ParentId",
                table: "ContractorCategory",
                column: "ParentId",
                principalTable: "ContractorCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractCategory_ContractCategory_ParentId",
                table: "ContractCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractorCategory_ContractorCategory_ParentId",
                table: "ContractorCategory");

            migrationBuilder.DropIndex(
                name: "IX_ContractorCategory_ParentId",
                table: "ContractorCategory");

            migrationBuilder.DropIndex(
                name: "IX_ContractCategory_ParentId",
                table: "ContractCategory");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ContractorCategory");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ContractCategory");
        }
    }
}
