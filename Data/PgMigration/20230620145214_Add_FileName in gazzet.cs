using Microsoft.EntityFrameworkCore.Migrations;

namespace Advocate.Data.PgMigration
{
    public partial class Add_FileNameingazzet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "file_name",
                table: "Mst_GazetteData",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_name",
                table: "Mst_GazetteData");
        }
    }
}
