using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addfk_RepairService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_RepairService_AssignTo",
                table: "RepairService",
                column: "AssignTo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairService_Users_AssignTo",
                table: "RepairService",
                column: "AssignTo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairService_Users_AssignTo",
                table: "RepairService");

            migrationBuilder.DropIndex(
                name: "IX_RepairService_AssignTo",
                table: "RepairService");

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 18, 10, 10, 305, DateTimeKind.Local).AddTicks(9079));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 18, 10, 10, 305, DateTimeKind.Local).AddTicks(9133));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 18, 10, 10, 305, DateTimeKind.Local).AddTicks(9137));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 18, 10, 10, 305, DateTimeKind.Local).AddTicks(9141));
        }
    }
}
