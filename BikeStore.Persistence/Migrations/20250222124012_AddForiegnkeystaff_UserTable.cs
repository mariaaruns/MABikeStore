using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddForiegnkeystaff_UserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "staff_id",
            //    schema: "sales",
            //    table: "staffs",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_staffs_Users_staff_id",
                schema: "sales",
                table: "staffs",
                column: "staff_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_staffs_Users_staff_id",
                schema: "sales",
                table: "staffs");

            //migrationBuilder.AlterColumn<int>(
            //    name: "staff_id",
            //    schema: "sales",
            //    table: "staffs",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 16, 14, 47, 51, 267, DateTimeKind.Local).AddTicks(5030));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 16, 14, 47, 51, 267, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 16, 14, 47, 51, 267, DateTimeKind.Local).AddTicks(5075));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 16, 14, 47, 51, 267, DateTimeKind.Local).AddTicks(5079));
        }
    }
}
