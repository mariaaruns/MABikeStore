using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modifiedRepairServicetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairService",
                table: "RepairService");

            migrationBuilder.DropColumn(
                name: "Issues",
                table: "RepairService");

            migrationBuilder.EnsureSchema(
                name: "Service");

            migrationBuilder.RenameTable(
                name: "RepairService",
                newName: "RepairService",
                newSchema: "Service");

            migrationBuilder.AddPrimaryKey(
                name: "PK__RepairService__5E5A8E27729211BE",
                schema: "Service",
                table: "RepairService",
                column: "ServiceId");

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invoice__5E5A8E27729211BE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_RepairService_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Service",
                        principalTable: "RepairService",
                        principalColumn: "ServiceId");
                });

            migrationBuilder.CreateTable(
                name: "RepairIssues",
                schema: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepairServiceId = table.Column<int>(type: "int", nullable: false),
                    IssueDescription = table.Column<string>(type: "varchar(1000)", nullable: false),
                    IssueFixed = table.Column<bool>(type: "bit", nullable: true),
                    FixedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RepairIssues__5E5A8E27729211BE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairIssues_RepairService_RepairServiceId",
                        column: x => x.RepairServiceId,
                        principalSchema: "Service",
                        principalTable: "RepairService",
                        principalColumn: "ServiceId");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                schema: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__InvoiceItems__5E5A8E27729211BE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "Service",
                        principalTable: "Invoice",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 1, 13, 47, 20, 162, DateTimeKind.Local).AddTicks(8646));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 1, 13, 47, 20, 162, DateTimeKind.Local).AddTicks(8684));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 1, 13, 47, 20, 162, DateTimeKind.Local).AddTicks(8688));

            migrationBuilder.UpdateData(
                table: "Lookup",
                keyColumn: "LookupId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 1, 13, 47, 20, 162, DateTimeKind.Local).AddTicks(8692));

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ServiceId",
                schema: "Service",
                table: "Invoice",
                column: "ServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                schema: "Service",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairIssues_RepairServiceId",
                schema: "Service",
                table: "RepairIssues",
                column: "RepairServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "RepairIssues",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK__RepairService__5E5A8E27729211BE",
                schema: "Service",
                table: "RepairService");

            migrationBuilder.RenameTable(
                name: "RepairService",
                schema: "Service",
                newName: "RepairService");

            migrationBuilder.AddColumn<string>(
                name: "Issues",
                table: "RepairService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairService",
                table: "RepairService",
                column: "ServiceId");

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
        }
    }
}
