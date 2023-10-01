using Microsoft.EntityFrameworkCore.Migrations;

namespace Advocate.Data.PgMigration
{
    public partial class updategazzetentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mst_GazetteData_Mst_GazetteType_GazzetTypeId",
                table: "Mst_GazetteData");

            migrationBuilder.DropIndex(
                name: "IX_Mst_GazetteData_GazzetTypeId",
                table: "Mst_GazetteData");

            migrationBuilder.RenameColumn(
                name: "GazzetTypeId",
                table: "Mst_GazetteData",
                newName: "gazzetTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "gazzetTypeId",
                table: "Mst_GazetteData",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gazzetTypeId",
                table: "Mst_GazetteData",
                newName: "GazzetTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "GazzetTypeId",
                table: "Mst_GazetteData",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_GazetteData_GazzetTypeId",
                table: "Mst_GazetteData",
                column: "GazzetTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mst_GazetteData_Mst_GazetteType_GazzetTypeId",
                table: "Mst_GazetteData",
                column: "GazzetTypeId",
                principalTable: "Mst_GazetteType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
