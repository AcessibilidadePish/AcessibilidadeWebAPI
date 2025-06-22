using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessibilidadeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnSenhaEDeleteColumnDeficienteTableUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ehDeficiente",
                table: "usuario");

            migrationBuilder.AddColumn<string>(
                name: "senha",
                table: "usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "senha",
                table: "usuario");

            migrationBuilder.AddColumn<bool>(
                name: "ehDeficiente",
                table: "usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
