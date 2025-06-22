using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessibilidadeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTableDeficiente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_deficiente_idUsuario",
                table: "deficiente",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deficiente");
        }
    }
}
