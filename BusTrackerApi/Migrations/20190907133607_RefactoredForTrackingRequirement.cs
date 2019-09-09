using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTrackerApi.Migrations
{
    public partial class RefactoredForTrackingRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusTrackers_Places_LastDestinationId",
                table: "BusTrackers");

            migrationBuilder.AlterColumn<Guid>(
                name: "LastDestinationId",
                table: "BusTrackers",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "CurrentRouteStatus",
                table: "BusTrackers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GDirection",
                table: "BusTrackers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StartLattitude",
                table: "BusTrackers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StartLongitude",
                table: "BusTrackers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_BusTrackers_Places_LastDestinationId",
                table: "BusTrackers",
                column: "LastDestinationId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusTrackers_Places_LastDestinationId",
                table: "BusTrackers");

            migrationBuilder.DropColumn(
                name: "CurrentRouteStatus",
                table: "BusTrackers");

            migrationBuilder.DropColumn(
                name: "GDirection",
                table: "BusTrackers");

            migrationBuilder.DropColumn(
                name: "StartLattitude",
                table: "BusTrackers");

            migrationBuilder.DropColumn(
                name: "StartLongitude",
                table: "BusTrackers");

            migrationBuilder.AlterColumn<Guid>(
                name: "LastDestinationId",
                table: "BusTrackers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusTrackers_Places_LastDestinationId",
                table: "BusTrackers",
                column: "LastDestinationId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
