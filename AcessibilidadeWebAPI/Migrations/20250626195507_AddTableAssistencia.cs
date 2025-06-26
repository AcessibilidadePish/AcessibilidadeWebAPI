using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessibilidadeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTableAssistencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "assistencia",
                columns: table => new
                {
                    idAssistencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSolicitacaoAjuda = table.Column<int>(type: "int", nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    dataAceite = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    dataConclusao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assistencia", x => x.idAssistencia);
                    table.ForeignKey(
                        name: "FK_assistencia_idSolicitacaoAjuda_solicitacaoAjuda_idSolicitacaoAjuda",
                        column: x => x.idSolicitacaoAjuda,
                        principalTable: "solicitacaoAjuda",
                        principalColumn: "idSolicitacaoAjuda");
                    table.ForeignKey(
                        name: "FK_assistencia_idUsuario_deficiente_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "deficiente",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_assistencia_idSolicitacaoAjuda",
                table: "assistencia",
                column: "idSolicitacaoAjuda");

            migrationBuilder.CreateIndex(
                name: "IX_assistencia_idUsuario",
                table: "assistencia",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assistencia");
        }
    }
}
