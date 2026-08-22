using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStatementAndExtraDeductionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractorStatement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTypeStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatementDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorStatement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorStatement_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractorStatement_StepContractType_ContractTypeStepId",
                        column: x => x.ContractTypeStepId,
                        principalTable: "StepContractType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtraOrDeductionType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsExtra = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraOrDeductionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtraOrDeductionRule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractorType = table.Column<int>(type: "int", nullable: true),
                    ExtraOrDeductionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraOrDeductionRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraOrDeductionRule_ContractType_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraOrDeductionRule_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraOrDeductionRule_ContractorCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ContractorCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraOrDeductionRule_Contractor_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraOrDeductionRule_ExtraOrDeductionType_ExtraOrDeductionTypeId",
                        column: x => x.ExtraOrDeductionTypeId,
                        principalTable: "ExtraOrDeductionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractorStatementExtraOrDeduction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorStatementExtraOrDeduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorStatementExtraOrDeduction_ContractorStatement_StatementId",
                        column: x => x.StatementId,
                        principalTable: "ContractorStatement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorStatementExtraOrDeduction_ExtraOrDeductionRule_RuleId",
                        column: x => x.RuleId,
                        principalTable: "ExtraOrDeductionRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatement_ContractId",
                table: "ContractorStatement",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatement_ContractTypeStepId",
                table: "ContractorStatement",
                column: "ContractTypeStepId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatement_IsDeleted",
                table: "ContractorStatement",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatementExtraOrDeduction_IsDeleted",
                table: "ContractorStatementExtraOrDeduction",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatementExtraOrDeduction_RuleId",
                table: "ContractorStatementExtraOrDeduction",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatementExtraOrDeduction_StatementId",
                table: "ContractorStatementExtraOrDeduction",
                column: "StatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionRule_CategoryId",
                table: "ExtraOrDeductionRule",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionRule_ContractId",
                table: "ExtraOrDeductionRule",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionRule_ContractorId",
                table: "ExtraOrDeductionRule",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionRule_ContractTypeId",
                table: "ExtraOrDeductionRule",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionRule_ExtraOrDeductionTypeId",
                table: "ExtraOrDeductionRule",
                column: "ExtraOrDeductionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionRule_IsDeleted",
                table: "ExtraOrDeductionRule",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionType_IsActive",
                table: "ExtraOrDeductionType",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionType_IsDeleted",
                table: "ExtraOrDeductionType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionType_Title",
                table: "ExtraOrDeductionType",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorStatementExtraOrDeduction");

            migrationBuilder.DropTable(
                name: "ContractorStatement");

            migrationBuilder.DropTable(
                name: "ExtraOrDeductionRule");

            migrationBuilder.DropTable(
                name: "ExtraOrDeductionType");
        }
    }
}
