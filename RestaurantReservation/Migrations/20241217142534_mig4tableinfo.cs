using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Migrations
{
    /// <inheritdoc />
    public partial class mig4tableinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Restaurants",
                newName: "RId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "CId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RId",
                table: "Restaurants",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CId",
                table: "Customers",
                newName: "Id");
        }
    }
}
