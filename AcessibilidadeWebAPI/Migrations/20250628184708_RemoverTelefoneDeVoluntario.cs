using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessibilidadeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoverTelefoneDeVoluntario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "telefone",
                table: "voluntario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "telefone",
                table: "voluntario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
