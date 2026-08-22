using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class change_label_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_StepContractType_Contract_ContractId",
                table: "Contract_StepContractType");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_StepContractType_StepContractType_StepId",
                table: "Contract_StepContractType");

            migrationBuilder.DropForeignKey(
                name: "FK_StepContractType_ContractType_ContractTypeId",
                table: "StepContractType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityLabel",
                table: "EntityLabel");

            migrationBuilder.DropIndex(
                name: "IX_EntityLabel_LabelId_EntityId",
                table: "EntityLabel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StepContractType",
                table: "StepContractType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract_StepContractType",
                table: "Contract_StepContractType");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EntityLabel");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EntityLabel");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EntityLabel");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EntityLabel");

            migrationBuilder.RenameTable(
                name: "StepContractType",
                newName: "ContractTypeStep");

            migrationBuilder.RenameTable(
                name: "Contract_StepContractType",
                newName: "Contract_ContractTypeStep");

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

            migrationBuilder.RenameIndex(
                name: "IX_Contract_StepContractType_StepId",
                table: "Contract_ContractTypeStep",
                newName: "IX_Contract_ContractTypeStep_StepId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_StepContractType_ContractId",
                table: "Contract_ContractTypeStep",
                newName: "IX_Contract_ContractTypeStep_ContractId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityLabel",
                table: "EntityLabel",
                columns: new[] { "LabelId", "EntityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractTypeStep",
                table: "ContractTypeStep",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract_ContractTypeStep",
                table: "Contract_ContractTypeStep",
                columns: new[] { "ContractId", "StepId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractTypeStep_ContractTypeStep_StepId",
                table: "Contract_ContractTypeStep",
                column: "StepId",
                principalTable: "ContractTypeStep",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractTypeStep_Contract_ContractId",
                table: "Contract_ContractTypeStep",
                column: "ContractId",
                principalTable: "Contract",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractTypeStep_ContractTypeStep_StepId",
                table: "Contract_ContractTypeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractTypeStep_Contract_ContractId",
                table: "Contract_ContractTypeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractTypeStep_ContractType_ContractTypeId",
                table: "ContractTypeStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityLabel",
                table: "EntityLabel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractTypeStep",
                table: "ContractTypeStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract_ContractTypeStep",
                table: "Contract_ContractTypeStep");

            migrationBuilder.RenameTable(
                name: "ContractTypeStep",
                newName: "StepContractType");

            migrationBuilder.RenameTable(
                name: "Contract_ContractTypeStep",
                newName: "Contract_StepContractType");

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

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ContractTypeStep_StepId",
                table: "Contract_StepContractType",
                newName: "IX_Contract_StepContractType_StepId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ContractTypeStep_ContractId",
                table: "Contract_StepContractType",
                newName: "IX_Contract_StepContractType_ContractId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "EntityLabel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EntityLabel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EntityLabel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EntityLabel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityLabel",
                table: "EntityLabel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StepContractType",
                table: "StepContractType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract_StepContractType",
                table: "Contract_StepContractType",
                columns: new[] { "ContractId", "StepId" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityLabel_LabelId_EntityId",
                table: "EntityLabel",
                columns: new[] { "LabelId", "EntityId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_StepContractType_Contract_ContractId",
                table: "Contract_StepContractType",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_StepContractType_StepContractType_StepId",
                table: "Contract_StepContractType",
                column: "StepId",
                principalTable: "StepContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StepContractType_ContractType_ContractTypeId",
                table: "StepContractType",
                column: "ContractTypeId",
                principalTable: "ContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
