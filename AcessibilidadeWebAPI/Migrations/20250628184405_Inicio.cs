using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessibilidadeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "local",
                columns: table => new
                {
                    idLocal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    latitude = table.Column<int>(type: "int", nullable: false),
                    longitude = table.Column<int>(type: "int", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avaliacaoAcessibilidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_local", x => x.idLocal);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "deficiente",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    tipoDeficiencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deficiente", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_deficiente_idUsuario_usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "dispositivo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioProprietarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispositivo", x => x.id);
                    table.ForeignKey(
                        name: "FK_dispositivo_usuarioProprietarioId_usuario_idUsuario",
                        column: x => x.usuarioProprietarioId,
                        principalTable: "usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "voluntario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    disponivel = table.Column<bool>(type: "bit", nullable: false),
                    avaliacao = table.Column<decimal>(type: "decimal(3,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voluntario", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_voluntario_idUsuario_usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "solicitacaoAjuda",
                columns: table => new
                {
                    idSolicitacaoAjuda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    dataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataResposta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    latitude = table.Column<double>(type: "float", nullable: true),
                    longitude = table.Column<double>(type: "float", nullable: true),
                    enderecoReferencia = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "avaliacaoLocal",
                columns: table => new
                {
                    idAvaliacaoLocal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idLocal = table.Column<int>(type: "int", nullable: false),
                    idDispositivo = table.Column<int>(type: "int", nullable: false),
                    acessivel = table.Column<bool>(type: "bit", nullable: false),
                    observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avaliacaoLocal", x => x.idAvaliacaoLocal);
                    table.ForeignKey(
                        name: "FK_avaliacaoLocal_idDispositivo_dispositivo_id",
                        column: x => x.idDispositivo,
                        principalTable: "dispositivo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_avaliacaoLocal_idLocal_local_idLocal",
                        column: x => x.idLocal,
                        principalTable: "local",
                        principalColumn: "idLocal");
                });

            migrationBuilder.CreateTable(
                name: "assistencia",
                columns: table => new
                {
                    idAssistencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSolicitacaoAjuda = table.Column<int>(type: "int", nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    dataAceite = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    dataConclusao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeficienteIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assistencia", x => x.idAssistencia);
                    table.ForeignKey(
                        name: "FK_assistencia_deficiente_DeficienteIdUsuario",
                        column: x => x.DeficienteIdUsuario,
                        principalTable: "deficiente",
                        principalColumn: "idUsuario");
                    table.ForeignKey(
                        name: "FK_assistencia_idSolicitacaoAjuda_solicitacaoAjuda_idSolicitacaoAjuda",
                        column: x => x.idSolicitacaoAjuda,
                        principalTable: "solicitacaoAjuda",
                        principalColumn: "idSolicitacaoAjuda");
                    table.ForeignKey(
                        name: "FK_assistencia_idUsuario_voluntario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "voluntario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "historicoStatusSolicitacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    solicitacaoAjudaId = table.Column<int>(type: "int", nullable: false),
                    statusAnterior = table.Column<int>(type: "int", nullable: false),
                    statusAtual = table.Column<int>(type: "int", nullable: false),
                    dataMudanca = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historicoStatusSolicitacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_historicoStatusSolicitacao_solicitacaoAjudaId_solicitacaoAjuda_idSolicitacaoAjuda",
                        column: x => x.solicitacaoAjudaId,
                        principalTable: "solicitacaoAjuda",
                        principalColumn: "idSolicitacaoAjuda");
                });

            migrationBuilder.CreateIndex(
                name: "IX_assistencia_DeficienteIdUsuario",
                table: "assistencia",
                column: "DeficienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_assistencia_idSolicitacaoAjuda",
                table: "assistencia",
                column: "idSolicitacaoAjuda");

            migrationBuilder.CreateIndex(
                name: "IX_assistencia_idUsuario",
                table: "assistencia",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_avaliacaoLocal_idDispositivo",
                table: "avaliacaoLocal",
                column: "idDispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_avaliacaoLocal_idLocal",
                table: "avaliacaoLocal",
                column: "idLocal");

            migrationBuilder.CreateIndex(
                name: "IX_deficiente_idUsuario",
                table: "deficiente",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_dispositivo_usuarioProprietarioId",
                table: "dispositivo",
                column: "usuarioProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_historicoStatusSolicitacao_solicitacaoAjudaId",
                table: "historicoStatusSolicitacao",
                column: "solicitacaoAjudaId");

            migrationBuilder.CreateIndex(
                name: "IX_solicitacaoAjuda_idUsuario",
                table: "solicitacaoAjuda",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_voluntario_idUsuario",
                table: "voluntario",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assistencia");

            migrationBuilder.DropTable(
                name: "avaliacaoLocal");

            migrationBuilder.DropTable(
                name: "historicoStatusSolicitacao");

            migrationBuilder.DropTable(
                name: "voluntario");

            migrationBuilder.DropTable(
                name: "dispositivo");

            migrationBuilder.DropTable(
                name: "local");

            migrationBuilder.DropTable(
                name: "solicitacaoAjuda");

            migrationBuilder.DropTable(
                name: "deficiente");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
