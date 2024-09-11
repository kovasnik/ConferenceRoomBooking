using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceRoomBooking.Migrations
{
    // delete not needet booking id 
    public partial class DeleteBookingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_service_booking_BookingId",
                schema: "public",
                table: "service");

            migrationBuilder.DropIndex(
                name: "IX_service_BookingId",
                schema: "public",
                table: "service");

            migrationBuilder.DropColumn(
                name: "BookingId",
                schema: "public",
                table: "service");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                schema: "public",
                table: "service",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_service_BookingId",
                schema: "public",
                table: "service",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_service_booking_BookingId",
                schema: "public",
                table: "service",
                column: "BookingId",
                principalSchema: "public",
                principalTable: "booking",
                principalColumn: "id");
        }
    }
}
