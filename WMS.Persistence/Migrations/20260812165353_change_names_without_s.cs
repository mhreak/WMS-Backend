using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class change_names_without_s : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities");

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
                name: "FK_ContractCategoryMap_ContractCategories_CategoriesId",
                table: "ContractCategoryMap");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractorCategoryMap_ContractorCategories_CategoriesId",
                table: "ContractorCategoryMap");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractorCategoryMap_Contractors_ContractorsId",
                table: "ContractorCategoryMap");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_Cities_CityId",
                table: "Contractors");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityAttachment_Files_FileAssetId",
                table: "EntityAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_CountryId",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_ContractType_ContractTypeId",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Step",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contractors",
                table: "Contractors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractorCategories",
                table: "ContractorCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractCategories",
                table: "ContractCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract_ContractTypeStep",
                table: "Contract_ContractTypeStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.RenameTable(
                name: "Step",
                newName: "StepContractType");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "Province");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "File");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Contractors",
                newName: "Contractor");

            migrationBuilder.RenameTable(
                name: "ContractorCategories",
                newName: "ContractorCategory");

            migrationBuilder.RenameTable(
                name: "ContractCategories",
                newName: "ContractCategory");

            migrationBuilder.RenameTable(
                name: "Contract_ContractTypeStep",
                newName: "Contract_StepContractType");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameIndex(
                name: "IX_Step_IsDeleted",
                table: "StepContractType",
                newName: "IX_StepContractType_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Step_IsActive",
                table: "StepContractType",
                newName: "IX_StepContractType_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Step_ContractTypeId_StepOrder",
                table: "StepContractType",
                newName: "IX_StepContractType_ContractTypeId_StepOrder");

            migrationBuilder.RenameIndex(
                name: "IX_Step_ContractTypeId",
                table: "StepContractType",
                newName: "IX_StepContractType_ContractTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_CountryId",
                table: "Province",
                newName: "IX_Province_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_UploaderId",
                table: "File",
                newName: "IX_File_UploaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_IsDeleted",
                table: "File",
                newName: "IX_File_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_Type",
                table: "Contractor",
                newName: "IX_Contractor_Type");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_NationalCode",
                table: "Contractor",
                newName: "IX_Contractor_NationalCode");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_Mobile1",
                table: "Contractor",
                newName: "IX_Contractor_Mobile1");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_IsDeleted",
                table: "Contractor",
                newName: "IX_Contractor_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_IsActive",
                table: "Contractor",
                newName: "IX_Contractor_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_EconomicCode",
                table: "Contractor",
                newName: "IX_Contractor_EconomicCode");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_CityId",
                table: "Contractor",
                newName: "IX_Contractor_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorCategories_Name",
                table: "ContractorCategory",
                newName: "IX_ContractorCategory_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorCategories_IsDeleted",
                table: "ContractorCategory",
                newName: "IX_ContractorCategory_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorCategories_IsActive",
                table: "ContractorCategory",
                newName: "IX_ContractorCategory_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_ContractCategories_Name",
                table: "ContractCategory",
                newName: "IX_ContractCategory_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ContractCategories_IsDeleted",
                table: "ContractCategory",
                newName: "IX_ContractCategory_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ContractCategories_IsActive",
                table: "ContractCategory",
                newName: "IX_ContractCategory_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ContractTypeStep_StepId",
                table: "Contract_StepContractType",
                newName: "IX_Contract_StepContractType_StepId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ContractTypeStep_ContractId",
                table: "Contract_StepContractType",
                newName: "IX_Contract_StepContractType_ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_ProvinceId",
                table: "City",
                newName: "IX_City_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_CountryId",
                table: "City",
                newName: "IX_City_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StepContractType",
                table: "StepContractType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Province",
                table: "Province",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_File",
                table: "File",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contractor",
                table: "Contractor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractorCategory",
                table: "ContractorCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractCategory",
                table: "ContractCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract_StepContractType",
                table: "Contract_StepContractType",
                columns: new[] { "ContractId", "StepId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Country_CountryId",
                table: "City",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_City_Province_ProvinceId",
                table: "City",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Contractor_ContractorId",
                table: "Contract",
                column: "ContractorId",
                principalTable: "Contractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_ContractCategoryMap_ContractCategory_CategoriesId",
                table: "ContractCategoryMap",
                column: "CategoriesId",
                principalTable: "ContractCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractor_City_CityId",
                table: "Contractor",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorCategoryMap_ContractorCategory_CategoriesId",
                table: "ContractorCategoryMap",
                column: "CategoriesId",
                principalTable: "ContractorCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorCategoryMap_Contractor_ContractorsId",
                table: "ContractorCategoryMap",
                column: "ContractorsId",
                principalTable: "Contractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityAttachment_File_FileAssetId",
                table: "EntityAttachment",
                column: "FileAssetId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Province_Country_CountryId",
                table: "Province",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StepContractType_ContractType_ContractTypeId",
                table: "StepContractType",
                column: "ContractTypeId",
                principalTable: "ContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Country_CountryId",
                table: "City");

            migrationBuilder.DropForeignKey(
                name: "FK_City_Province_ProvinceId",
                table: "City");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Contractor_ContractorId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_StepContractType_Contract_ContractId",
                table: "Contract_StepContractType");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_StepContractType_StepContractType_StepId",
                table: "Contract_StepContractType");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractCategoryMap_ContractCategory_CategoriesId",
                table: "ContractCategoryMap");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractor_City_CityId",
                table: "Contractor");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractorCategoryMap_ContractorCategory_CategoriesId",
                table: "ContractorCategoryMap");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractorCategoryMap_Contractor_ContractorsId",
                table: "ContractorCategoryMap");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityAttachment_File_FileAssetId",
                table: "EntityAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Province_Country_CountryId",
                table: "Province");

            migrationBuilder.DropForeignKey(
                name: "FK_StepContractType_ContractType_ContractTypeId",
                table: "StepContractType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StepContractType",
                table: "StepContractType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Province",
                table: "Province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_File",
                table: "File");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractorCategory",
                table: "ContractorCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contractor",
                table: "Contractor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractCategory",
                table: "ContractCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract_StepContractType",
                table: "Contract_StepContractType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.RenameTable(
                name: "StepContractType",
                newName: "Step");

            migrationBuilder.RenameTable(
                name: "Province",
                newName: "Provinces");

            migrationBuilder.RenameTable(
                name: "File",
                newName: "Files");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "ContractorCategory",
                newName: "ContractorCategories");

            migrationBuilder.RenameTable(
                name: "Contractor",
                newName: "Contractors");

            migrationBuilder.RenameTable(
                name: "ContractCategory",
                newName: "ContractCategories");

            migrationBuilder.RenameTable(
                name: "Contract_StepContractType",
                newName: "Contract_ContractTypeStep");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameIndex(
                name: "IX_StepContractType_IsDeleted",
                table: "Step",
                newName: "IX_Step_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_StepContractType_IsActive",
                table: "Step",
                newName: "IX_Step_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_StepContractType_ContractTypeId_StepOrder",
                table: "Step",
                newName: "IX_Step_ContractTypeId_StepOrder");

            migrationBuilder.RenameIndex(
                name: "IX_StepContractType_ContractTypeId",
                table: "Step",
                newName: "IX_Step_ContractTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Province_CountryId",
                table: "Provinces",
                newName: "IX_Provinces_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_File_UploaderId",
                table: "Files",
                newName: "IX_Files_UploaderId");

            migrationBuilder.RenameIndex(
                name: "IX_File_IsDeleted",
                table: "Files",
                newName: "IX_Files_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorCategory_Name",
                table: "ContractorCategories",
                newName: "IX_ContractorCategories_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorCategory_IsDeleted",
                table: "ContractorCategories",
                newName: "IX_ContractorCategories_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorCategory_IsActive",
                table: "ContractorCategories",
                newName: "IX_ContractorCategories_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Contractor_Type",
                table: "Contractors",
                newName: "IX_Contractors_Type");

            migrationBuilder.RenameIndex(
                name: "IX_Contractor_NationalCode",
                table: "Contractors",
                newName: "IX_Contractors_NationalCode");

            migrationBuilder.RenameIndex(
                name: "IX_Contractor_Mobile1",
                table: "Contractors",
                newName: "IX_Contractors_Mobile1");

            migrationBuilder.RenameIndex(
                name: "IX_Contractor_IsDeleted",
                table: "Contractors",
                newName: "IX_Contractors_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Contractor_IsActive",
                table: "Contractors",
                newName: "IX_Contractors_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Contractor_EconomicCode",
                table: "Contractors",
                newName: "IX_Contractors_EconomicCode");

            migrationBuilder.RenameIndex(
                name: "IX_Contractor_CityId",
                table: "Contractors",
                newName: "IX_Contractors_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractCategory_Name",
                table: "ContractCategories",
                newName: "IX_ContractCategories_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ContractCategory_IsDeleted",
                table: "ContractCategories",
                newName: "IX_ContractCategories_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ContractCategory_IsActive",
                table: "ContractCategories",
                newName: "IX_ContractCategories_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_StepContractType_StepId",
                table: "Contract_ContractTypeStep",
                newName: "IX_Contract_ContractTypeStep_StepId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_StepContractType_ContractId",
                table: "Contract_ContractTypeStep",
                newName: "IX_Contract_ContractTypeStep_ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_City_ProvinceId",
                table: "Cities",
                newName: "IX_Cities_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_City_CountryId",
                table: "Cities",
                newName: "IX_Cities_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Step",
                table: "Step",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractorCategories",
                table: "ContractorCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contractors",
                table: "Contractors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractCategories",
                table: "ContractCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract_ContractTypeStep",
                table: "Contract_ContractTypeStep",
                columns: new[] { "ContractId", "StepId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_ContractCategoryMap_ContractCategories_CategoriesId",
                table: "ContractCategoryMap",
                column: "CategoriesId",
                principalTable: "ContractCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorCategoryMap_ContractorCategories_CategoriesId",
                table: "ContractorCategoryMap",
                column: "CategoriesId",
                principalTable: "ContractorCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorCategoryMap_Contractors_ContractorsId",
                table: "ContractorCategoryMap",
                column: "ContractorsId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_Cities_CityId",
                table: "Contractors",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityAttachment_Files_FileAssetId",
                table: "EntityAttachment",
                column: "FileAssetId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Countries_CountryId",
                table: "Provinces",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_ContractType_ContractTypeId",
                table: "Step",
                column: "ContractTypeId",
                principalTable: "ContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
