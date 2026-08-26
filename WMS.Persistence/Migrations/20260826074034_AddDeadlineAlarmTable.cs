using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeadlineAlarmTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LabelId",
                table: "ExtraOrDeductionRule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ContractorStatement",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DeadlineAlarm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferenceDateType = table.Column<int>(type: "int", nullable: false),
                    DaysOffset = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeadlineAlarm", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrDeductionRule_LabelId",
                table: "ExtraOrDeductionRule",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_DeadlineAlarm_EntityType_EntityId_StepId",
                table: "DeadlineAlarm",
                columns: new[] { "EntityType", "EntityId", "StepId" });

            migrationBuilder.CreateIndex(
                name: "IX_DeadlineAlarm_IsActive",
                table: "DeadlineAlarm",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_DeadlineAlarm_IsDeleted",
                table: "DeadlineAlarm",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraOrDeductionRule_Label_LabelId",
                table: "ExtraOrDeductionRule",
                column: "LabelId",
                principalTable: "Label",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraOrDeductionRule_Label_LabelId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.DropTable(
                name: "DeadlineAlarm");

            migrationBuilder.DropIndex(
                name: "IX_ExtraOrDeductionRule_LabelId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "ExtraOrDeductionRule");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ContractorStatement");
        }
    }
}
