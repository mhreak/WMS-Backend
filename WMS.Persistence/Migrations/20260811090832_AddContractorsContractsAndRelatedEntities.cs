using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractorsContractsAndRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractAmount = table.Column<long>(type: "bigint", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FinishedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StepOrder = table.Column<short>(type: "smallint", nullable: false),
                    ContractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steps_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractCategoryMap",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCategoryMap", x => new { x.CategoriesId, x.ContractsId });
                    table.ForeignKey(
                        name: "FK_ContractCategoryMap_ContractCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "ContractCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractCategoryMap_Contracts_ContractsId",
                        column: x => x.ContractsId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract_ContractTypeSteps",
                columns: table => new
                {
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FinishDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract_ContractTypeSteps", x => new { x.ContractId, x.StepId });
                    table.ForeignKey(
                        name: "FK_Contract_ContractTypeSteps_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_ContractTypeSteps_Steps_StepId",
                        column: x => x.StepId,
                        principalTable: "Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractTypeSteps_ContractId",
                table: "Contract_ContractTypeSteps",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractTypeSteps_StepId",
                table: "Contract_ContractTypeSteps",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCategories_IsActive",
                table: "ContractCategories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCategories_IsDeleted",
                table: "ContractCategories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCategories_Name",
                table: "ContractCategories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCategoryMap_ContractsId",
                table: "ContractCategoryMap",
                column: "ContractsId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractNumber",
                table: "Contracts",
                column: "ContractNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractorId",
                table: "Contracts",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IsDeleted",
                table: "Contracts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_StartDate",
                table: "Contracts",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_ContractTypeId",
                table: "Steps",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_ContractTypeId_StepOrder",
                table: "Steps",
                columns: new[] { "ContractTypeId", "StepOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Steps_IsActive",
                table: "Steps",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_IsDeleted",
                table: "Steps",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract_ContractTypeSteps");

            migrationBuilder.DropTable(
                name: "ContractCategoryMap");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "ContractCategories");

            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
