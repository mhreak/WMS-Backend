using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameAlarmToNotification_NewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeadlineAlarm");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    FieldIndicator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PeriodicCheck = table.Column<bool>(type: "bit", nullable: false),
                    Config = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_EntityType",
                table: "Notification",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_IsActive",
                table: "Notification",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_IsDeleted",
                table: "Notification",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.CreateTable(
                name: "DeadlineAlarm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaysOffset = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceDateType = table.Column<int>(type: "int", nullable: false),
                    StepId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeadlineAlarm", x => x.Id);
                });

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
        }
    }
}
