using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LivrariaAPI.Migrations
{
    public partial class Intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<int>(type: "int", nullable: true),
                    Autor = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Data_Publicacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url_Imagem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro");
        }
    }
}
