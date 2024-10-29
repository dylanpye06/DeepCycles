using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeepCycles.Migrations
{
    /// <inheritdoc />
    public partial class FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HandmadeBikes",
                columns: table => new
                {
                    BikeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BikeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BikeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandmadeBikes", x => x.BikeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HandmadeBikes");
        }
    }
}
