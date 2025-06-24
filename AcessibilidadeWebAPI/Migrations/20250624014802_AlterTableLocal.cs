using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessibilidadeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Local",
                table: "Local");

            migrationBuilder.RenameTable(
                name: "Local",
                newName: "local");

            migrationBuilder.AddPrimaryKey(
                name: "PK_local",
                table: "local",
                column: "idLocal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_local",
                table: "local");

            migrationBuilder.RenameTable(
                name: "local",
                newName: "Local");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Local",
                table: "Local",
                column: "idLocal");
        }
    }
}
