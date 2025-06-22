using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessibilidadeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTableVoluntario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "voluntario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    disponivel = table.Column<bool>(type: "bit", nullable: false),
                    avaliacao = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_voluntario_idUsuario",
                table: "voluntario",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "voluntario");
        }
    }
}
