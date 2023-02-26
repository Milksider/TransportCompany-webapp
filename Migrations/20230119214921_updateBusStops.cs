using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportCompany.Migrations
{
    public partial class updateBusStops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transportRoutes_busStops_busStopId",
                table: "transportRoutes");

            migrationBuilder.DropColumn(
                name: "EndBusStop",
                table: "transportRoutes");

            migrationBuilder.RenameColumn(
                name: "busStopId",
                table: "transportRoutes",
                newName: "StartBusStopId");

            migrationBuilder.RenameColumn(
                name: "StartBusStop",
                table: "transportRoutes",
                newName: "EndBusStopId");

            migrationBuilder.RenameIndex(
                name: "IX_transportRoutes_busStopId",
                table: "transportRoutes",
                newName: "IX_transportRoutes_StartBusStopId");

            migrationBuilder.CreateIndex(
                name: "IX_transportRoutes_EndBusStopId",
                table: "transportRoutes",
                column: "EndBusStopId");

            migrationBuilder.AddForeignKey(
                name: "FK_transportRoutes_busStops_EndBusStopId",
                table: "transportRoutes",
                column: "EndBusStopId",
                principalTable: "busStops",
                principalColumn: "Id"
                );

            migrationBuilder.AddForeignKey(
                name: "FK_transportRoutes_busStops_StartBusStopId",
                table: "transportRoutes",
                column: "StartBusStopId",
                principalTable: "busStops",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transportRoutes_busStops_EndBusStopId",
                table: "transportRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_transportRoutes_busStops_StartBusStopId",
                table: "transportRoutes");

            migrationBuilder.DropIndex(
                name: "IX_transportRoutes_EndBusStopId",
                table: "transportRoutes");

            migrationBuilder.RenameColumn(
                name: "StartBusStopId",
                table: "transportRoutes",
                newName: "busStopId");

            migrationBuilder.RenameColumn(
                name: "EndBusStopId",
                table: "transportRoutes",
                newName: "StartBusStop");

            migrationBuilder.RenameIndex(
                name: "IX_transportRoutes_StartBusStopId",
                table: "transportRoutes",
                newName: "IX_transportRoutes_busStopId");

            migrationBuilder.AddColumn<int>(
                name: "EndBusStop",
                table: "transportRoutes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_transportRoutes_busStops_busStopId",
                table: "transportRoutes",
                column: "busStopId",
                principalTable: "busStops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
