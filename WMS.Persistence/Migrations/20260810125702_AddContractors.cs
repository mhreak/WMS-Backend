using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractorCategories",
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
                    table.PrimaryKey("PK_ContractorCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Mobile1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Mobile2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EconomicCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contractors_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contractors_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileAssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityAttachments_Files_FileAssetId",
                        column: x => x.FileAssetId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractorCategoryMap",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorCategoryMap", x => new { x.CategoriesId, x.ContractorsId });
                    table.ForeignKey(
                        name: "FK_ContractorCategoryMap_ContractorCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "ContractorCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorCategoryMap_Contractors_ContractorsId",
                        column: x => x.ContractorsId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityLabels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityLabels_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorCategories_IsActive",
                table: "ContractorCategories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorCategories_IsDeleted",
                table: "ContractorCategories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorCategories_Name",
                table: "ContractorCategories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorCategoryMap_ContractorsId",
                table: "ContractorCategoryMap",
                column: "ContractorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_CityId",
                table: "Contractors",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_EconomicCode",
                table: "Contractors",
                column: "EconomicCode");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_IsActive",
                table: "Contractors",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_IsDeleted",
                table: "Contractors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_Mobile1",
                table: "Contractors",
                column: "Mobile1");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_NationalCode",
                table: "Contractors",
                column: "NationalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_ProvinceId",
                table: "Contractors",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_Type",
                table: "Contractors",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypes_IsActive",
                table: "ContractTypes",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypes_IsDeleted",
                table: "ContractTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypes_Title",
                table: "ContractTypes",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachments_EntityId",
                table: "EntityAttachments",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachments_EntityId_EntityType",
                table: "EntityAttachments",
                columns: new[] { "EntityId", "EntityType" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachments_EntityType",
                table: "EntityAttachments",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachments_FileAssetId",
                table: "EntityAttachments",
                column: "FileAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttachments_FileAssetId_EntityId_EntityType",
                table: "EntityAttachments",
                columns: new[] { "FileAssetId", "EntityId", "EntityType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityLabels_EntityId",
                table: "EntityLabels",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityLabels_LabelId",
                table: "EntityLabels",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityLabels_LabelId_EntityId",
                table: "EntityLabels",
                columns: new[] { "LabelId", "EntityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Labels_EntityType",
                table: "Labels",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_IsActive",
                table: "Labels",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_IsDeleted",
                table: "Labels",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_Name_EntityType",
                table: "Labels",
                columns: new[] { "Name", "EntityType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorCategoryMap");

            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropTable(
                name: "EntityAttachments");

            migrationBuilder.DropTable(
                name: "EntityLabels");

            migrationBuilder.DropTable(
                name: "ContractorCategories");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "Labels");
        }
    }
}
