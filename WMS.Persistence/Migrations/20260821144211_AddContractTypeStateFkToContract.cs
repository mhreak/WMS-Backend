using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractTypeStateFkToContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContractTypeStateId",
                table: "Contract",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractTypeStateId",
                table: "Contract",
                column: "ContractTypeStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractTypeState_ContractTypeStateId",
                table: "Contract",
                column: "ContractTypeStateId",
                principalTable: "ContractTypeState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractTypeState_ContractTypeStateId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_ContractTypeStateId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "ContractTypeStateId",
                table: "Contract");
        }
    }
}
