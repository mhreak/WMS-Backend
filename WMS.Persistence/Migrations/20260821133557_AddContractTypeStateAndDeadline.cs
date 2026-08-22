using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractTypeStateAndDeadline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractTypeStep_ContractTypeStep_StepId",
                table: "Contract_ContractTypeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractTypeStep_ContractType_ContractTypeId",
                table: "ContractTypeStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractTypeStep",
                table: "ContractTypeStep");

            migrationBuilder.RenameTable(
                name: "ContractTypeStep",
                newName: "StepContractType");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTypeStep_IsDeleted",
                table: "StepContractType",
                newName: "IX_StepContractType_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTypeStep_IsActive",
                table: "StepContractType",
                newName: "IX_StepContractType_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTypeStep_ContractTypeId_StepOrder",
                table: "StepContractType",
                newName: "IX_StepContractType_ContractTypeId_StepOrder");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTypeStep_ContractTypeId",
                table: "StepContractType",
                newName: "IX_StepContractType_ContractTypeId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DeadlineDate",
                table: "Contract_ContractTypeStep",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EndStateId",
                table: "StepContractType",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartStateId",
                table: "StepContractType",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StepContractType",
                table: "StepContractType",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContractTypeState",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypeState", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractTypeStep_DeadlineDate",
                table: "Contract_ContractTypeStep",
                column: "DeadlineDate");

            migrationBuilder.CreateIndex(
                name: "IX_StepContractType_EndStateId",
                table: "StepContractType",
                column: "EndStateId");

            migrationBuilder.CreateIndex(
                name: "IX_StepContractType_StartStateId",
                table: "StepContractType",
                column: "StartStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeState_IsActive",
                table: "ContractTypeState",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeState_IsDeleted",
                table: "ContractTypeState",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeState_Title",
                table: "ContractTypeState",
                column: "Title");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractTypeStep_StepContractType_StepId",
                table: "Contract_ContractTypeStep",
                column: "StepId",
                principalTable: "StepContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StepContractType_ContractTypeState_EndStateId",
                table: "StepContractType",
                column: "EndStateId",
                principalTable: "ContractTypeState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StepContractType_ContractTypeState_StartStateId",
                table: "StepContractType",
                column: "StartStateId",
                principalTable: "ContractTypeState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StepContractType_ContractType_ContractTypeId",
                table: "StepContractType",
                column: "ContractTypeId",
                principalTable: "ContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractTypeStep_StepContractType_StepId",
                table: "Contract_ContractTypeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_StepContractType_ContractTypeState_EndStateId",
                table: "StepContractType");

            migrationBuilder.DropForeignKey(
                name: "FK_StepContractType_ContractTypeState_StartStateId",
                table: "StepContractType");

            migrationBuilder.DropForeignKey(
                name: "FK_StepContractType_ContractType_ContractTypeId",
                table: "StepContractType");

            migrationBuilder.DropTable(
                name: "ContractTypeState");

            migrationBuilder.DropIndex(
                name: "IX_Contract_ContractTypeStep_DeadlineDate",
                table: "Contract_ContractTypeStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StepContractType",
                table: "StepContractType");

            migrationBuilder.DropIndex(
                name: "IX_StepContractType_EndStateId",
                table: "StepContractType");

            migrationBuilder.DropIndex(
                name: "IX_StepContractType_StartStateId",
                table: "StepContractType");

            migrationBuilder.DropColumn(
                name: "DeadlineDate",
                table: "Contract_ContractTypeStep");

            migrationBuilder.DropColumn(
                name: "EndStateId",
                table: "StepContractType");

            migrationBuilder.DropColumn(
                name: "StartStateId",
                table: "StepContractType");

            migrationBuilder.RenameTable(
                name: "StepContractType",
                newName: "ContractTypeStep");

            migrationBuilder.RenameIndex(
                name: "IX_StepContractType_IsDeleted",
                table: "ContractTypeStep",
                newName: "IX_ContractTypeStep_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_StepContractType_IsActive",
                table: "ContractTypeStep",
                newName: "IX_ContractTypeStep_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_StepContractType_ContractTypeId_StepOrder",
                table: "ContractTypeStep",
                newName: "IX_ContractTypeStep_ContractTypeId_StepOrder");

            migrationBuilder.RenameIndex(
                name: "IX_StepContractType_ContractTypeId",
                table: "ContractTypeStep",
                newName: "IX_ContractTypeStep_ContractTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractTypeStep",
                table: "ContractTypeStep",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractTypeStep_ContractTypeStep_StepId",
                table: "Contract_ContractTypeStep",
                column: "StepId",
                principalTable: "ContractTypeStep",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTypeStep_ContractType_ContractTypeId",
                table: "ContractTypeStep",
                column: "ContractTypeId",
                principalTable: "ContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
