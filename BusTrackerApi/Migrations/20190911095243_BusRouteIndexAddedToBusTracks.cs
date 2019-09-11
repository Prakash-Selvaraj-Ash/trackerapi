using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTrackerApi.Migrations
{
    public partial class BusRouteIndexAddedToBusTracks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusTrackers_BusId",
                table: "BusTrackers");

            migrationBuilder.CreateIndex(
                name: "IX_BusTrackers_BusId_RouteId",
                table: "BusTrackers",
                columns: new[] { "BusId", "RouteId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusTrackers_BusId_RouteId",
                table: "BusTrackers");

            migrationBuilder.CreateIndex(
                name: "IX_BusTrackers_BusId",
                table: "BusTrackers",
                column: "BusId");
        }
    }
}
