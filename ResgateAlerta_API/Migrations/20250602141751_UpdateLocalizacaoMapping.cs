using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoDenuncia.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLocalizacaoMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_LOCALIZACAO_TBL_BAIRRO_IdBairro",
                table: "TBL_LOCALIZACAO");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "TBL_LOCALIZACAO",
                newName: "numero");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "TBL_LOCALIZACAO",
                newName: "logradouro");

            migrationBuilder.RenameColumn(
                name: "Complemento",
                table: "TBL_LOCALIZACAO",
                newName: "complemento");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "TBL_LOCALIZACAO",
                newName: "cep");

            migrationBuilder.RenameColumn(
                name: "IdBairro",
                table: "TBL_LOCALIZACAO",
                newName: "id_bairro");

            migrationBuilder.RenameColumn(
                name: "IdLocalização",
                table: "TBL_LOCALIZACAO",
                newName: "id_localizacao");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_LOCALIZACAO_IdBairro",
                table: "TBL_LOCALIZACAO",
                newName: "IX_TBL_LOCALIZACAO_id_bairro");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_LOCALIZACAO_TBL_BAIRRO_id_bairro",
                table: "TBL_LOCALIZACAO",
                column: "id_bairro",
                principalTable: "TBL_BAIRRO",
                principalColumn: "id_bairro",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_LOCALIZACAO_TBL_BAIRRO_id_bairro",
                table: "TBL_LOCALIZACAO");

            migrationBuilder.RenameColumn(
                name: "numero",
                table: "TBL_LOCALIZACAO",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "logradouro",
                table: "TBL_LOCALIZACAO",
                newName: "Logradouro");

            migrationBuilder.RenameColumn(
                name: "complemento",
                table: "TBL_LOCALIZACAO",
                newName: "Complemento");

            migrationBuilder.RenameColumn(
                name: "cep",
                table: "TBL_LOCALIZACAO",
                newName: "Cep");

            migrationBuilder.RenameColumn(
                name: "id_bairro",
                table: "TBL_LOCALIZACAO",
                newName: "IdBairro");

            migrationBuilder.RenameColumn(
                name: "id_localizacao",
                table: "TBL_LOCALIZACAO",
                newName: "IdLocalização");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_LOCALIZACAO_id_bairro",
                table: "TBL_LOCALIZACAO",
                newName: "IX_TBL_LOCALIZACAO_IdBairro");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_LOCALIZACAO_TBL_BAIRRO_IdBairro",
                table: "TBL_LOCALIZACAO",
                column: "IdBairro",
                principalTable: "TBL_BAIRRO",
                principalColumn: "id_bairro",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
