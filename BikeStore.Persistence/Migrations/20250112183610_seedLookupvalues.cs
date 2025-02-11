using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BikeStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seedLookupvalues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lookup",
                columns: new[] { "LookupId", "CreatedDate", "IsActive", "LookupName", "LookupValue" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 13, 0, 6, 9, 27, DateTimeKind.Local).AddTicks(1042), true, "Order Status", "Order Placed" },
                    { 2, new DateTime(2025, 1, 13, 0, 6, 9, 27, DateTimeKind.Local).AddTicks(1084), true, "Order Status", "In Progress" },
                    { 3, new DateTime(2025, 1, 13, 0, 6, 9, 27, DateTimeKind.Local).AddTicks(1087), true, "Order Status", "Ready for Pickup/Delivery" },
                    { 4, new DateTime(2025, 1, 13, 0, 6, 9, 27, DateTimeKind.Local).AddTicks(1090), true, "Order Status", "Completed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 4);
        }
    }
}
