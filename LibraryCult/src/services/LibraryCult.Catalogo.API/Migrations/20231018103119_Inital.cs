using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryCult.Catalogo.API.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetUtcDate()"),
                    Image = table.Column<string>(type: "varchar(100)", nullable: true),
                    WareHouseQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
