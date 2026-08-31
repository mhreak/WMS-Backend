using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addedcontracttypeid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContractTypeId",
                table: "Contract",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractTypeId",
                table: "Contract",
                column: "ContractTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractType_ContractTypeId",
                table: "Contract",
                column: "ContractTypeId",
                principalTable: "ContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractType_ContractTypeId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_ContractTypeId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "ContractTypeId",
                table: "Contract");
        }
    }
}
