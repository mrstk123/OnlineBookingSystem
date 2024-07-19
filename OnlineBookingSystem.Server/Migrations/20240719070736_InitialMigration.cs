using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineBookingSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    Transmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Availability = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    BookingNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentalRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RentalStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentalEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Phone", "UserName" },
                values: new object[,]
                {
                    { 1, "admin@mail.com", "admin", "", new byte[] { 158, 2, 221, 18, 185, 47, 162, 199, 33, 139, 191, 244, 99, 127, 77, 206, 23, 208, 79, 37, 171, 122, 111, 208, 92, 89, 240, 107, 79, 236, 108, 29, 109, 72, 46, 229, 140, 175, 41, 35, 40, 101, 166, 226, 96, 41, 155, 13, 191, 173, 180, 147, 218, 134, 232, 59, 123, 136, 44, 38, 139, 44, 16, 145 }, new byte[] { 243, 39, 118, 135, 47, 140, 88, 109, 49, 1, 132, 115, 185, 31, 181, 38, 1, 45, 35, 131, 194, 65, 210, 144, 241, 163, 64, 148, 181, 72, 167, 15, 194, 171, 101, 168, 224, 227, 206, 227, 234, 156, 28, 79, 67, 37, 234, 5, 138, 19, 252, 111, 88, 141, 147, 124, 64, 82, 187, 169, 250, 218, 89, 221, 16, 97, 142, 148, 250, 245, 254, 23, 21, 202, 105, 92, 134, 14, 199, 136, 155, 161, 188, 105, 67, 242, 87, 3, 224, 124, 145, 117, 218, 49, 176, 85, 243, 201, 28, 5, 37, 136, 156, 187, 90, 153, 244, 77, 6, 129, 126, 189, 32, 96, 132, 3, 166, 82, 174, 248, 27, 107, 154, 233, 138, 159, 175, 233 }, "", "admin" },
                    { 2, "user@mail.com", "user", "", new byte[] { 70, 200, 61, 29, 220, 5, 134, 139, 127, 56, 226, 52, 71, 120, 173, 188, 28, 159, 15, 251, 159, 65, 251, 82, 88, 7, 98, 81, 154, 53, 238, 186, 40, 151, 67, 107, 70, 57, 38, 70, 241, 77, 36, 193, 64, 84, 221, 107, 167, 21, 82, 88, 8, 230, 165, 116, 232, 180, 228, 236, 125, 27, 3, 156 }, new byte[] { 227, 167, 187, 244, 130, 121, 13, 41, 255, 160, 170, 70, 62, 41, 134, 51, 195, 36, 241, 154, 242, 240, 141, 75, 34, 54, 80, 132, 26, 155, 165, 39, 183, 248, 34, 207, 47, 96, 193, 137, 27, 113, 113, 138, 242, 184, 13, 252, 18, 206, 3, 167, 11, 191, 229, 197, 69, 118, 28, 14, 67, 230, 152, 250, 244, 215, 125, 57, 40, 142, 68, 49, 166, 207, 76, 251, 0, 58, 150, 3, 79, 103, 141, 235, 18, 117, 227, 204, 241, 241, 127, 15, 114, 44, 95, 255, 152, 84, 142, 97, 219, 24, 14, 44, 13, 122, 211, 205, 161, 158, 188, 120, 89, 121, 26, 93, 23, 28, 76, 217, 255, 218, 6, 99, 120, 120, 250, 41 }, "", "user" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
