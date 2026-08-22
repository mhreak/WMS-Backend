using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class change_names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractTypeSteps_Contracts_ContractId",
                table: "Contract_ContractTypeSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractTypeSteps_Steps_StepId",
                table: "Contract_ContractTypeSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractCategoryMap_Contracts_ContractsId",
                table: "ContractCategoryMap");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Contractors_ContractorId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityAttachments_Files_FileAssetId",
                table: "EntityAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityLabels_Labels_LabelId",
                table: "EntityLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_ContractTypes_ContractTypeId",
                table: "Steps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Steps",
                table: "Steps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labels",
                table: "Labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityLabels",
                table: "EntityLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityAttachments",
                table: "EntityAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractTypes",
                table: "ContractTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract_ContractTypeSteps",
                table: "Contract_ContractTypeSteps");

            migrationBuilder.RenameTable(
                name: "Steps",
                newName: "Step");

            migrationBuilder.RenameTable(
                name: "Labels",
                newName: "Label");

            migrationBuilder.RenameTable(
                name: "EntityLabels",
                newName: "EntityLabel");

            migrationBuilder.RenameTable(
                name: "EntityAttachments",
                newName: "EntityAttachment");

            migrationBuilder.RenameTable(
                name: "ContractTypes",
                newName: "ContractType");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contract");

            migrationBuilder.RenameTable(
                name: "Contract_ContractTypeSteps",
                newName: "Contract_ContractTypeStep");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_IsDeleted",
                table: "Step",
                newName: "IX_Step_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_IsActive",
                table: "Step",
                newName: "IX_Step_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_ContractTypeId_StepOrder",
                table: "Step",
                newName: "IX_Step_ContractTypeId_StepOrder");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_ContractTypeId",
                table: "Step",
                newName: "IX_Step_ContractTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_Name_EntityType",
                table: "Label",
                newName: "IX_Label_Name_EntityType");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_IsDeleted",
                table: "Label",
                newName: "IX_Label_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_IsActive",
                table: "Label",
                newName: "IX_Label_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_EntityType",
                table: "Label",
                newName: "IX_Label_EntityType");

            migrationBuilder.RenameIndex(
                name: "IX_EntityLabels_LabelId_EntityId",
                table: "EntityLabel",
                newName: "IX_EntityLabel_LabelId_EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityLabels_LabelId",
                table: "EntityLabel",
                newName: "IX_EntityLabel_LabelId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityLabels_EntityId",
                table: "EntityLabel",
                newName: "IX_EntityLabel_EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachments_FileAssetId_EntityId_EntityType",
                table: "EntityAttachment",
                newName: "IX_EntityAttachment_FileAssetId_EntityId_EntityType");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachments_FileAssetId",
                table: "EntityAttachment",
                newName: "IX_EntityAttachment_FileAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachments_EntityType",
                table: "EntityAttachment",
                newName: "IX_EntityAttachment_EntityType");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachments_EntityId_EntityType",
                table: "EntityAttachment",
                newName: "IX_EntityAttachment_EntityId_EntityType");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachments_EntityId",
                table: "EntityAttachment",
                newName: "IX_EntityAttachment_EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTypes_Title",
                table: "ContractType",
                newName: "IX_ContractType_Title");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTypes_IsDeleted",
                table: "ContractType",
                newName: "IX_ContractType_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTypes_IsActive",
                table: "ContractType",
                newName: "IX_ContractType_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_StartDate",
                table: "Contract",
                newName: "IX_Contract_StartDate");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_IsDeleted",
                table: "Contract",
                newName: "IX_Contract_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_ContractorId",
                table: "Contract",
                newName: "IX_Contract_ContractorId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_ContractNumber",
                table: "Contract",
                newName: "IX_Contract_ContractNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ContractTypeSteps_StepId",
                table: "Contract_ContractTypeStep",
                newName: "IX_Contract_ContractTypeStep_StepId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ContractTypeSteps_ContractId",
                table: "Contract_ContractTypeStep",
                newName: "IX_Contract_ContractTypeStep_ContractId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Step",
                table: "Step",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Label",
                table: "Label",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityLabel",
                table: "EntityLabel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityAttachment",
                table: "EntityAttachment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractType",
                table: "ContractType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract_ContractTypeStep",
                table: "Contract_ContractTypeStep",
                columns: new[] { "ContractId", "StepId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Contractors_ContractorId",
                table: "Contract",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractTypeStep_Contract_ContractId",
                table: "Contract_ContractTypeStep",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractTypeStep_Step_StepId",
                table: "Contract_ContractTypeStep",
                column: "StepId",
                principalTable: "Step",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCategoryMap_Contract_ContractsId",
                table: "ContractCategoryMap",
                column: "ContractsId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityAttachment_Files_FileAssetId",
                table: "EntityAttachment",
                column: "FileAssetId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityLabel_Label_LabelId",
                table: "EntityLabel",
                column: "LabelId",
                principalTable: "Label",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_ContractType_ContractTypeId",
                table: "Step",
                column: "ContractTypeId",
                principalTable: "ContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Contractors_ContractorId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractTypeStep_Contract_ContractId",
                table: "Contract_ContractTypeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractTypeStep_Step_StepId",
                table: "Contract_ContractTypeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractCategoryMap_Contract_ContractsId",
                table: "ContractCategoryMap");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityAttachment_Files_FileAssetId",
                table: "EntityAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityLabel_Label_LabelId",
                table: "EntityLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_ContractType_ContractTypeId",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Step",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Label",
                table: "Label");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityLabel",
                table: "EntityLabel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityAttachment",
                table: "EntityAttachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractType",
                table: "ContractType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract_ContractTypeStep",
                table: "Contract_ContractTypeStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                table: "Contract");

            migrationBuilder.RenameTable(
                name: "Step",
                newName: "Steps");

            migrationBuilder.RenameTable(
                name: "Label",
                newName: "Labels");

            migrationBuilder.RenameTable(
                name: "EntityLabel",
                newName: "EntityLabels");

            migrationBuilder.RenameTable(
                name: "EntityAttachment",
                newName: "EntityAttachments");

            migrationBuilder.RenameTable(
                name: "ContractType",
                newName: "ContractTypes");

            migrationBuilder.RenameTable(
                name: "Contract_ContractTypeStep",
                newName: "Contract_ContractTypeSteps");

            migrationBuilder.RenameTable(
                name: "Contract",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_Step_IsDeleted",
                table: "Steps",
                newName: "IX_Steps_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Step_IsActive",
                table: "Steps",
                newName: "IX_Steps_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Step_ContractTypeId_StepOrder",
                table: "Steps",
                newName: "IX_Steps_ContractTypeId_StepOrder");

            migrationBuilder.RenameIndex(
                name: "IX_Step_ContractTypeId",
                table: "Steps",
                newName: "IX_Steps_ContractTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Label_Name_EntityType",
                table: "Labels",
                newName: "IX_Labels_Name_EntityType");

            migrationBuilder.RenameIndex(
                name: "IX_Label_IsDeleted",
                table: "Labels",
                newName: "IX_Labels_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Label_IsActive",
                table: "Labels",
                newName: "IX_Labels_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Label_EntityType",
                table: "Labels",
                newName: "IX_Labels_EntityType");

            migrationBuilder.RenameIndex(
                name: "IX_EntityLabel_LabelId_EntityId",
                table: "EntityLabels",
                newName: "IX_EntityLabels_LabelId_EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityLabel_LabelId",
                table: "EntityLabels",
                newName: "IX_EntityLabels_LabelId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityLabel_EntityId",
                table: "EntityLabels",
                newName: "IX_EntityLabels_EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachment_FileAssetId_EntityId_EntityType",
                table: "EntityAttachments",
                newName: "IX_EntityAttachments_FileAssetId_EntityId_EntityType");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachment_FileAssetId",
                table: "EntityAttachments",
                newName: "IX_EntityAttachments_FileAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachment_EntityType",
                table: "EntityAttachments",
                newName: "IX_EntityAttachments_EntityType");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachment_EntityId_EntityType",
                table: "EntityAttachments",
                newName: "IX_EntityAttachments_EntityId_EntityType");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAttachment_EntityId",
                table: "EntityAttachments",
                newName: "IX_EntityAttachments_EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractType_Title",
                table: "ContractTypes",
                newName: "IX_ContractTypes_Title");

            migrationBuilder.RenameIndex(
                name: "IX_ContractType_IsDeleted",
                table: "ContractTypes",
                newName: "IX_ContractTypes_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ContractType_IsActive",
                table: "ContractTypes",
                newName: "IX_ContractTypes_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ContractTypeStep_StepId",
                table: "Contract_ContractTypeSteps",
                newName: "IX_Contract_ContractTypeSteps_StepId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ContractTypeStep_ContractId",
                table: "Contract_ContractTypeSteps",
                newName: "IX_Contract_ContractTypeSteps_ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_StartDate",
                table: "Contracts",
                newName: "IX_Contracts_StartDate");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_IsDeleted",
                table: "Contracts",
                newName: "IX_Contracts_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ContractorId",
                table: "Contracts",
                newName: "IX_Contracts_ContractorId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ContractNumber",
                table: "Contracts",
                newName: "IX_Contracts_ContractNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Steps",
                table: "Steps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labels",
                table: "Labels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityLabels",
                table: "EntityLabels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityAttachments",
                table: "EntityAttachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractTypes",
                table: "ContractTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract_ContractTypeSteps",
                table: "Contract_ContractTypeSteps",
                columns: new[] { "ContractId", "StepId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractTypeSteps_Contracts_ContractId",
                table: "Contract_ContractTypeSteps",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractTypeSteps_Steps_StepId",
                table: "Contract_ContractTypeSteps",
                column: "StepId",
                principalTable: "Steps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCategoryMap_Contracts_ContractsId",
                table: "ContractCategoryMap",
                column: "ContractsId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Contractors_ContractorId",
                table: "Contracts",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityAttachments_Files_FileAssetId",
                table: "EntityAttachments",
                column: "FileAssetId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityLabels_Labels_LabelId",
                table: "EntityLabels",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_ContractTypes_ContractTypeId",
                table: "Steps",
                column: "ContractTypeId",
                principalTable: "ContractTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
