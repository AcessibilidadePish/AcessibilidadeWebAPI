using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessibilidadeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTableSolicitacaoAjuda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "solicitacaoAjuda",
                columns: table => new
                {
                    idSolicitacaoAjuda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    dataSolicitacao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    dataResposta = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solicitacaoAjuda", x => x.idSolicitacaoAjuda);
                    table.ForeignKey(
                        name: "FK_solicitacaoAjudae_idUsuario_deficiente_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "deficiente",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_solicitacaoAjuda_idUsuario",
                table: "solicitacaoAjuda",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "solicitacaoAjuda");
        }
    }
}
