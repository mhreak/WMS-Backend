using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class delete_stepOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractTypeStep_ContractTypeId_StepOrder",
                table: "ContractTypeStep");

            migrationBuilder.DropColumn(
                name: "StepOrder",
                table: "ContractTypeStep");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "StepOrder",
                table: "ContractTypeStep",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeStep_ContractTypeId_StepOrder",
                table: "ContractTypeStep",
                columns: new[] { "ContractTypeId", "StepOrder" });
        }
    }
}
