using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGM.OrdemServico.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdemServico",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdSolicitante = table.Column<Guid>(nullable: false),
                    IdSolicitacao = table.Column<Guid>(nullable: true),
                    CodDepartamento = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    Solucao = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataSolucao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemServico", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdemServico");
        }
    }
}
