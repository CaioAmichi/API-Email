using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailApi.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeRemetente = table.Column<Guid>(type: "uniqueidentifier", maxLength: 100, nullable: false),
                    NomeEmpresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailRemetente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SenhaRemetente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Smtp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
