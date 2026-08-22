using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReAddStepOrderAndFixFinishDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "StepOrder",
                table: "ContractTypeStep",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FinishDate",
                table: "Contract_ContractTypeStep",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeStep_ContractTypeId_StepOrder",
                table: "ContractTypeStep",
                columns: new[] { "ContractTypeId", "StepOrder" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractTypeStep_ContractTypeId_StepOrder",
                table: "ContractTypeStep");

            migrationBuilder.DropColumn(
                name: "StepOrder",
                table: "ContractTypeStep");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FinishDate",
                table: "Contract_ContractTypeStep",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
