using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessibilidadeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTableAvaliacaoLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "avaliacaoLocal",
                columns: table => new
                {
                    IdAvaliacaoLocal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idLocal = table.Column<int>(type: "int", nullable: false),
                    acessivel = table.Column<bool>(type: "bit", nullable: false),
                    observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timestamp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avaliacaoLocal", x => x.IdAvaliacaoLocal);
                    table.ForeignKey(
                        name: "FK_avaliacaoLocal_idLocal_local_idLocal",
                        column: x => x.idLocal,
                        principalTable: "local",
                        principalColumn: "idLocal");
                });

            migrationBuilder.CreateIndex(
                name: "IX_avaliacaoLocal_idLocal",
                table: "avaliacaoLocal",
                column: "idLocal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "avaliacaoLocal");
        }
    }
}
