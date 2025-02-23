using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIssuesColumninRepairServicetbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Issues",
                table: "RepairService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 19, 49, 26, 499, DateTimeKind.Local).AddTicks(9350));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 19, 49, 26, 499, DateTimeKind.Local).AddTicks(9386));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 19, 49, 26, 499, DateTimeKind.Local).AddTicks(9390));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 19, 49, 26, 499, DateTimeKind.Local).AddTicks(9393));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Issues",
                table: "RepairService");

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 19, 25, 33, 562, DateTimeKind.Local).AddTicks(1398));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 19, 25, 33, 562, DateTimeKind.Local).AddTicks(1596));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 19, 25, 33, 562, DateTimeKind.Local).AddTicks(1601));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 19, 25, 33, 562, DateTimeKind.Local).AddTicks(1605));
        }
    }
}
