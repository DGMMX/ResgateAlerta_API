using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoDenuncia.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBairroMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_BAIRRO_TBL_CIDADE_IdCidade",
                table: "TBL_BAIRRO");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TBL_BAIRRO",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "IdCidade",
                table: "TBL_BAIRRO",
                newName: "id_cidade");

            migrationBuilder.RenameColumn(
                name: "IdBairro",
                table: "TBL_BAIRRO",
                newName: "id_bairro");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_BAIRRO_IdCidade",
                table: "TBL_BAIRRO",
                newName: "IX_TBL_BAIRRO_id_cidade");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_BAIRRO_TBL_CIDADE_id_cidade",
                table: "TBL_BAIRRO",
                column: "id_cidade",
                principalTable: "TBL_CIDADE",
                principalColumn: "id_cidade",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_BAIRRO_TBL_CIDADE_id_cidade",
                table: "TBL_BAIRRO");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "TBL_BAIRRO",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "id_cidade",
                table: "TBL_BAIRRO",
                newName: "IdCidade");

            migrationBuilder.RenameColumn(
                name: "id_bairro",
                table: "TBL_BAIRRO",
                newName: "IdBairro");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_BAIRRO_id_cidade",
                table: "TBL_BAIRRO",
                newName: "IX_TBL_BAIRRO_IdCidade");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_BAIRRO_TBL_CIDADE_IdCidade",
                table: "TBL_BAIRRO",
                column: "IdCidade",
                principalTable: "TBL_CIDADE",
                principalColumn: "id_cidade",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
