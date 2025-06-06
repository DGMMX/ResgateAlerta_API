using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResgateAlerta.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSumaryAcompanhamentoDenunciaEndpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TBL_USUARIOS_email",
                table: "TBL_USUARIOS",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TBL_USUARIOS_email",
                table: "TBL_USUARIOS");
        }
    }
}
