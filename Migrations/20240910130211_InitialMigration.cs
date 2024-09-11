using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConferenceRoomBooking.Migrations
{
    // initial migration
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "conference_room",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    cost_per_hour = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conference_room", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    room_id = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_cost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.id);
                    table.ForeignKey(
                        name: "FK_booking_conference_room_room_id",
                        column: x => x.room_id,
                        principalSchema: "public",
                        principalTable: "conference_room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "service",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    BookingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service", x => x.id);
                    table.ForeignKey(
                        name: "FK_service_booking_BookingId",
                        column: x => x.BookingId,
                        principalSchema: "public",
                        principalTable: "booking",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "room_serice",
                schema: "public",
                columns: table => new
                {
                    service_id = table.Column<int>(type: "integer", nullable: false),
                    room_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_serice", x => new { x.service_id, x.room_id });
                    table.ForeignKey(
                        name: "FK_room_serice_conference_room_room_id",
                        column: x => x.room_id,
                        principalSchema: "public",
                        principalTable: "conference_room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_room_serice_service_service_id",
                        column: x => x.service_id,
                        principalSchema: "public",
                        principalTable: "service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_room_id",
                schema: "public",
                table: "booking",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_serice_room_id",
                schema: "public",
                table: "room_serice",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_BookingId",
                schema: "public",
                table: "service",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "room_serice",
                schema: "public");

            migrationBuilder.DropTable(
                name: "service",
                schema: "public");

            migrationBuilder.DropTable(
                name: "booking",
                schema: "public");

            migrationBuilder.DropTable(
                name: "conference_room",
                schema: "public");
        }
    }
}
