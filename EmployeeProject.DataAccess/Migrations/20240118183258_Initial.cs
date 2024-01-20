using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmploymentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BossId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Adress = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Salary = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_BossId",
                        column: x => x.BossId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Worker" },
                    { 2, "Regular Employee" },
                    { 3, "Boss" },
                    { 4, "CEO" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Adress", "BirthDate", "BossId", "EmploymentDate", "FirstName", "LastName", "RoleId", "Salary" },
                values: new object[,]
                {
                    { new Guid("18fcf979-4804-4ca1-aeb2-3dbd355242d7"), "Vilnius Mira st. 15", new DateTime(1999, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2019, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pilypas", "Petrokas", 4, 15000 },
                    { new Guid("26f8ebe2-0e5c-4aa3-8cde-bf58cf2afc68"), "Vilnius Skila st. 25", new DateTime(1986, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2012, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vytis", "Kreivaitis", 3, 5000 },
                    { new Guid("d47cf8b6-1b6b-4e46-bbd7-59ca7fdaba14"), "Vilnius Vernik st. 51", new DateTime(1989, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2013, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salemonas", "Bauzys", 3, 5500 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BossId",
                table: "Employees",
                column: "BossId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
