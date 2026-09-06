using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveContracorid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraOrDeductionRule_Contractor_ContractorId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.DropIndex(
                name: "IX_ExtraOrDeductionRule_ContractorId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "ExtraOrDeductionRule");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContractorId",
                table: "ExtraOrDeductionRule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionRule_ContractorId",
                table: "ExtraOrDeductionRule",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraOrDeductionRule_Contractor_ContractorId",
                table: "ExtraOrDeductionRule",
                column: "ContractorId",
                principalTable: "Contractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
