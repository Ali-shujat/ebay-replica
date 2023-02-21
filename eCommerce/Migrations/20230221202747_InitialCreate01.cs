using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    public partial class InitialCreate01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buyer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "varchar(31)", unicode: false, maxLength: 31, nullable: false),
                    password = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    role = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    uniqueStoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(62)", unicode: false, maxLength: 62, nullable: false),
                    description = table.Column<string>(type: "varchar(552)", unicode: false, maxLength: 552, nullable: false),
                    imageUrl = table.Column<string>(type: "varchar(47)", unicode: false, maxLength: 47, nullable: false),
                    storeId = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    uniqueStoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Store__72E12F1AA1577EED", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buyer");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
