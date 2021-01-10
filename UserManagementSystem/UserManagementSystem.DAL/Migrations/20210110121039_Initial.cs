using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagementSystem.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    IsBlackListed = table.Column<bool>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsBlackListed", "Password", "Role", "Username" },
                values: new object[] { new Guid("e0306550-c717-4fd0-98da-08d8548aa120"), "admin@gmail.com", "Admin Admin", false, "Qwerty123", 0, "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsBlackListed", "Password", "Role", "Username" },
                values: new object[] { new Guid("a21bcb79-67a8-426a-98d6-08d8548aa120"), "georgi@gmail.com", "Georgi Gerdzhikov", false, "Qwerty123", 1, "georgi" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsBlackListed", "Password", "Role", "Username" },
                values: new object[] { new Guid("ca0a6dd8-518e-4ba9-98d9-08d8548aa120"), "mariya@gmail.com", "Mariya Ivanova", false, "Qwerty123", 1, "mariya" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
