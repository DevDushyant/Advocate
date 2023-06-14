using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Advocate.Data.PgMigration
{
    public partial class ExcelUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mst_Part_Mst_Gazette_GazettId",
                table: "Mst_Part");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mst_Gazette",
                table: "Mst_Gazette");

            migrationBuilder.RenameTable(
                name: "Mst_Gazette",
                newName: "Mst_GazetteType");

            migrationBuilder.RenameIndex(
                name: "IX_Mst_Gazette_GazetteName",
                table: "Mst_GazetteType",
                newName: "IX_Mst_GazetteType_GazetteName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mst_GazetteType",
                table: "Mst_GazetteType",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Mst_GazetteData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GazzetTypeId = table.Column<int>(type: "integer", nullable: true),
                    oraganization = table.Column<string>(type: "text", nullable: true),
                    department = table.Column<string>(type: "text", nullable: true),
                    office = table.Column<string>(type: "text", nullable: true),
                    subject = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: true),
                    part_section = table.Column<string>(type: "text", nullable: true),
                    issue_date = table.Column<string>(type: "text", nullable: true),
                    publish_date = table.Column<string>(type: "text", nullable: true),
                    reference_no = table.Column<string>(type: "text", nullable: true),
                    file_size = table.Column<string>(type: "text", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_GazetteData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_GazetteData_Mst_GazetteType_GazzetTypeId",
                        column: x => x.GazzetTypeId,
                        principalTable: "Mst_GazetteType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mst_GazetteData_GazzetTypeId",
                table: "Mst_GazetteData",
                column: "GazzetTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mst_Part_Mst_GazetteType_GazettId",
                table: "Mst_Part",
                column: "GazettId",
                principalTable: "Mst_GazetteType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mst_Part_Mst_GazetteType_GazettId",
                table: "Mst_Part");

            migrationBuilder.DropTable(
                name: "Mst_GazetteData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mst_GazetteType",
                table: "Mst_GazetteType");

            migrationBuilder.RenameTable(
                name: "Mst_GazetteType",
                newName: "Mst_Gazette");

            migrationBuilder.RenameIndex(
                name: "IX_Mst_GazetteType_GazetteName",
                table: "Mst_Gazette",
                newName: "IX_Mst_Gazette_GazetteName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mst_Gazette",
                table: "Mst_Gazette",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mst_Part_Mst_Gazette_GazettId",
                table: "Mst_Part",
                column: "GazettId",
                principalTable: "Mst_Gazette",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
