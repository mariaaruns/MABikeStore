using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairService_Users_AssignTo",
                table: "RepairService");

            migrationBuilder.DropIndex(
                name: "IX_RepairService_AssignTo",
                table: "RepairService");

            //migrationBuilder.AddColumn<int>(
            //    name: "ApplicationUserId",
            //    table: "UserRoles",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "RepairService",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateTable(
            //    name: "IdentityUserClaim<string>",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ApplicationUserId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_IdentityUserClaim<string>_Users_ApplicationUserId",
            //            column: x => x.ApplicationUserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "sales",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PaymentStatus = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpiTransactionId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UpiPayerVpa = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sales",
                        principalTable: "orders",
                        principalColumn: "order_id");
                });

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 23, 23, 5, 56, 349, DateTimeKind.Local).AddTicks(7949));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 23, 23, 5, 56, 349, DateTimeKind.Local).AddTicks(7985));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 23, 23, 5, 56, 349, DateTimeKind.Local).AddTicks(7991));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 23, 23, 5, 56, 349, DateTimeKind.Local).AddTicks(7994));

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserRoles_ApplicationUserId",
            //    table: "UserRoles",
            //    column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairService_AssignTo",
                table: "RepairService",
                column: "AssignTo");

            migrationBuilder.CreateIndex(
                name: "IX_RepairService_StoreId",
                table: "RepairService",
                column: "StoreId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_IdentityUserClaim<string>_ApplicationUserId",
            //    table: "IdentityUserClaim<string>",
            //    column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrderId",
                schema: "sales",
                table: "Payment",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairService_Users_AssignTo",
                table: "RepairService",
                column: "AssignTo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairService_stores_StoreId",
                table: "RepairService",
                column: "StoreId",
                principalSchema: "sales",
                principalTable: "stores",
                principalColumn: "store_id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserRoles_Users_ApplicationUserId",
            //    table: "UserRoles",
            //    column: "ApplicationUserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairService_Users_AssignTo",
                table: "RepairService");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairService_stores_StoreId",
                table: "RepairService");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserRoles_Users_ApplicationUserId",
            //    table: "UserRoles");

            //migrationBuilder.DropTable(
            //    name: "IdentityUserClaim<string>");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "sales");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserRoles_ApplicationUserId",
            //    table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_RepairService_AssignTo",
                table: "RepairService");

            migrationBuilder.DropIndex(
                name: "IX_RepairService_StoreId",
                table: "RepairService");

            //migrationBuilder.DropColumn(
            //    name: "ApplicationUserId",
            //    table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "RepairService");

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
    }
}
