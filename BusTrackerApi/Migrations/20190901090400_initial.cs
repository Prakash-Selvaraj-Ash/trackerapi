using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTrackerApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Lattitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SourceId = table.Column<Guid>(nullable: false),
                    DestinationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Places_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Routes_Places_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusTrackers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BusId = table.Column<Guid>(nullable: false),
                    RouteId = table.Column<Guid>(nullable: false),
                    LastDestinationId = table.Column<Guid>(nullable: false),
                    CurrentLattitude = table.Column<double>(nullable: false),
                    CurrentLongitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusTrackers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusTrackers_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusTrackers_Places_LastDestinationId",
                        column: x => x.LastDestinationId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusTrackers_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteAssociations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlaceId = table.Column<Guid>(nullable: false),
                    RouteId = table.Column<Guid>(nullable: false),
                    SequenceNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteAssociations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteAssociations_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteAssociations_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PlaceId = table.Column<Guid>(nullable: false),
                    RouteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiveTrackers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BusId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    ConnectionId = table.Column<string>(nullable: true),
                    DeviceFCMId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveTrackers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveTrackers_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiveTrackers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusTrackers_BusId",
                table: "BusTrackers",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_BusTrackers_LastDestinationId",
                table: "BusTrackers",
                column: "LastDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_BusTrackers_RouteId",
                table: "BusTrackers",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTrackers_BusId",
                table: "LiveTrackers",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTrackers_StudentId",
                table: "LiveTrackers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteAssociations_PlaceId",
                table: "RouteAssociations",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteAssociations_RouteId",
                table: "RouteAssociations",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_DestinationId",
                table: "Routes",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_SourceId",
                table: "Routes",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PlaceId",
                table: "Students",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_RouteId",
                table: "Students",
                column: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusTrackers");

            migrationBuilder.DropTable(
                name: "LiveTrackers");

            migrationBuilder.DropTable(
                name: "RouteAssociations");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
