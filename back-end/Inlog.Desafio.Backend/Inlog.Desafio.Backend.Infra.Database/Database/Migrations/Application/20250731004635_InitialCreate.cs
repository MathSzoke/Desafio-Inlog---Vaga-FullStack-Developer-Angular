using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inlog.Desafio.Backend.Infra.Database.Database.Migrations.Application
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Inlog");

            migrationBuilder.CreateTable(
                name: "Vehicles",
                schema: "Inlog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Chassis = table.Column<string>(type: "text", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    LicensePlate = table.Column<string>(type: "text", nullable: false),
                    TrackerSerialNumber = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    VehicleType = table.Column<int>(type: "integer", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Chassis",
                schema: "Inlog",
                table: "Vehicles",
                column: "Chassis",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles",
                schema: "Inlog");
        }
    }
}
