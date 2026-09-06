using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExtraOrDeductionRule_RemoveContract_AddCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraOrDeductionRule_Contract_ContractId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraOrDeductionRule_ContractorCategory_CategoryId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraOrDeductionRule_Label_LabelId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.RenameColumn(
                name: "LabelId",
                table: "ExtraOrDeductionRule",
                newName: "ContractorCategoryId");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "ExtraOrDeductionRule",
                newName: "ContractLabelId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ExtraOrDeductionRule",
                newName: "ContractCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraOrDeductionRule_LabelId",
                table: "ExtraOrDeductionRule",
                newName: "IX_ExtraOrDeductionRule_ContractorCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraOrDeductionRule_ContractId",
                table: "ExtraOrDeductionRule",
                newName: "IX_ExtraOrDeductionRule_ContractLabelId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraOrDeductionRule_CategoryId",
                table: "ExtraOrDeductionRule",
                newName: "IX_ExtraOrDeductionRule_ContractCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraOrDeductionRule_ContractCategory_ContractCategoryId",
                table: "ExtraOrDeductionRule",
                column: "ContractCategoryId",
                principalTable: "ContractCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraOrDeductionRule_ContractorCategory_ContractorCategoryId",
                table: "ExtraOrDeductionRule",
                column: "ContractorCategoryId",
                principalTable: "ContractorCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraOrDeductionRule_Label_ContractLabelId",
                table: "ExtraOrDeductionRule",
                column: "ContractLabelId",
                principalTable: "Label",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraOrDeductionRule_ContractCategory_ContractCategoryId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraOrDeductionRule_ContractorCategory_ContractorCategoryId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraOrDeductionRule_Label_ContractLabelId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.RenameColumn(
                name: "ContractorCategoryId",
                table: "ExtraOrDeductionRule",
                newName: "LabelId");

            migrationBuilder.RenameColumn(
                name: "ContractLabelId",
                table: "ExtraOrDeductionRule",
                newName: "ContractId");

            migrationBuilder.RenameColumn(
                name: "ContractCategoryId",
                table: "ExtraOrDeductionRule",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraOrDeductionRule_ContractorCategoryId",
                table: "ExtraOrDeductionRule",
                newName: "IX_ExtraOrDeductionRule_LabelId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraOrDeductionRule_ContractLabelId",
                table: "ExtraOrDeductionRule",
                newName: "IX_ExtraOrDeductionRule_ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraOrDeductionRule_ContractCategoryId",
                table: "ExtraOrDeductionRule",
                newName: "IX_ExtraOrDeductionRule_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraOrDeductionRule_Contract_ContractId",
                table: "ExtraOrDeductionRule",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraOrDeductionRule_ContractorCategory_CategoryId",
                table: "ExtraOrDeductionRule",
                column: "CategoryId",
                principalTable: "ContractorCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraOrDeductionRule_Label_LabelId",
                table: "ExtraOrDeductionRule",
                column: "LabelId",
                principalTable: "Label",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
