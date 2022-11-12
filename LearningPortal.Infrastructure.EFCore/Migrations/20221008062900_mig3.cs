using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPortal.Infrastructure.EFCore.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileImgId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblFileServers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HttpDomin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HttpPath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Capacity = table.Column<long>(type: "bigint", nullable: false),
                    FtpData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFileServers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFilePaths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FileServerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFilePaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFilePaths_tblFileServers_FileServerId",
                        column: x => x.FileServerId,
                        principalTable: "tblFileServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FilePathId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SizeOnDisk = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFiles_tblFilePaths_FilePathId",
                        column: x => x.FilePathId,
                        principalTable: "tblFilePaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCountry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FlagImgId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCountry_tblFiles_FlagImgId",
                        column: x => x.FlagImgId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCountryTranslates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountryTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCountryTranslates_tblCountry_LangId",
                        column: x => x.LangId,
                        principalTable: "tblCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblCountryTranslates_tblLanguage_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProvinces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProvinces_tblCountry_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCities_tblProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "tblProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProvinceTranslates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProvinceTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProvinceTranslates_tblLanguage_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProvinceTranslates_tblProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "tblProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblAddress_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblAddress_tblCities_CityId",
                        column: x => x.CityId,
                        principalTable: "tblCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblAddress_tblCountry_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblAddress_tblProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "tblProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCityTranslates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCityTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCityTranslates_tblCities_CityId",
                        column: x => x.CityId,
                        principalTable: "tblCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblCityTranslates_tblLanguage_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImgId",
                table: "AspNetUsers",
                column: "ProfileImgId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_CityId",
                table: "tblAddress",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_CountryId",
                table: "tblAddress",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_ProvinceId",
                table: "tblAddress",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_UserId",
                table: "tblAddress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCities_ProvinceId",
                table: "tblCities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCityTranslates_CityId",
                table: "tblCityTranslates",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCityTranslates_LangId",
                table: "tblCityTranslates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCountry_FlagImgId",
                table: "tblCountry",
                column: "FlagImgId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCountryTranslates_CountryId",
                table: "tblCountryTranslates",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCountryTranslates_LangId",
                table: "tblCountryTranslates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFilePaths_FileServerId",
                table: "tblFilePaths",
                column: "FileServerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFiles_FilePathId",
                table: "tblFiles",
                column: "FilePathId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFiles_UserId",
                table: "tblFiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProvinces_CountryId",
                table: "tblProvinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProvinceTranslates_LangId",
                table: "tblProvinceTranslates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProvinceTranslates_ProvinceId",
                table: "tblProvinceTranslates",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tblFiles_ProfileImgId",
                table: "AspNetUsers",
                column: "ProfileImgId",
                principalTable: "tblFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tblFiles_ProfileImgId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tblAddress");

            migrationBuilder.DropTable(
                name: "tblCityTranslates");

            migrationBuilder.DropTable(
                name: "tblCountryTranslates");

            migrationBuilder.DropTable(
                name: "tblProvinceTranslates");

            migrationBuilder.DropTable(
                name: "tblCities");

            migrationBuilder.DropTable(
                name: "tblProvinces");

            migrationBuilder.DropTable(
                name: "tblCountry");

            migrationBuilder.DropTable(
                name: "tblFiles");

            migrationBuilder.DropTable(
                name: "tblFilePaths");

            migrationBuilder.DropTable(
                name: "tblFileServers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImgId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImgId",
                table: "AspNetUsers");
        }
    }
}
