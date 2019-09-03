using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTrackerApi.Migrations
{
    public partial class addedfcmid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FcmId",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FcmId",
                table: "Students");
        }
    }
}
