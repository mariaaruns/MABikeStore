using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IsActivecategorycol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
              name: "IsActive",
              schema: "production",
              table: "categories",
              type: "bit",
              nullable: true);

            migrationBuilder.AddColumn<bool>(
              name: "IsActive",
              schema: "sales",
              table: "stores",
              type: "bit",
              nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "production",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "sales",
                table: "stores");

        }
    }
}
