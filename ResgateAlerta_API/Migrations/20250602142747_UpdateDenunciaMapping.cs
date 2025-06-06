using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoDenuncia.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDenunciaMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_LOCALIZACAO_IdLocalizacao",
                table: "TBL_DENUNCIAS");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAOS_PUBLICOS_IdOrgaoPublico",
                table: "TBL_DENUNCIAS");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_USUARIOS_IdUsuario",
                table: "TBL_DENUNCIAS");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "TBL_DENUNCIAS",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "TBL_DENUNCIAS",
                newName: "id_usuario");

            migrationBuilder.RenameColumn(
                name: "IdOrgaoPublico",
                table: "TBL_DENUNCIAS",
                newName: "id_orgao");

            migrationBuilder.RenameColumn(
                name: "IdLocalizacao",
                table: "TBL_DENUNCIAS",
                newName: "id_localizacao");

            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "TBL_DENUNCIAS",
                newName: "data_hora");

            migrationBuilder.RenameColumn(
                name: "IdDenuncia",
                table: "TBL_DENUNCIAS",
                newName: "id_denuncia");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_DENUNCIAS_IdUsuario",
                table: "TBL_DENUNCIAS",
                newName: "IX_TBL_DENUNCIAS_id_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_DENUNCIAS_IdOrgaoPublico",
                table: "TBL_DENUNCIAS",
                newName: "IX_TBL_DENUNCIAS_id_orgao");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_DENUNCIAS_IdLocalizacao",
                table: "TBL_DENUNCIAS",
                newName: "IX_TBL_DENUNCIAS_id_localizacao");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_LOCALIZACAO_id_localizacao",
                table: "TBL_DENUNCIAS",
                column: "id_localizacao",
                principalTable: "TBL_LOCALIZACAO",
                principalColumn: "id_localizacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAOS_PUBLICOS_id_orgao",
                table: "TBL_DENUNCIAS",
                column: "id_orgao",
                principalTable: "TBL_ORGAOS_PUBLICOS",
                principalColumn: "id_orgao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_USUARIOS_id_usuario",
                table: "TBL_DENUNCIAS",
                column: "id_usuario",
                principalTable: "TBL_USUARIOS",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_LOCALIZACAO_id_localizacao",
                table: "TBL_DENUNCIAS");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAOS_PUBLICOS_id_orgao",
                table: "TBL_DENUNCIAS");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_USUARIOS_id_usuario",
                table: "TBL_DENUNCIAS");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "TBL_DENUNCIAS",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "TBL_DENUNCIAS",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "id_orgao",
                table: "TBL_DENUNCIAS",
                newName: "IdOrgaoPublico");

            migrationBuilder.RenameColumn(
                name: "id_localizacao",
                table: "TBL_DENUNCIAS",
                newName: "IdLocalizacao");

            migrationBuilder.RenameColumn(
                name: "data_hora",
                table: "TBL_DENUNCIAS",
                newName: "DataHora");

            migrationBuilder.RenameColumn(
                name: "id_denuncia",
                table: "TBL_DENUNCIAS",
                newName: "IdDenuncia");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_DENUNCIAS_id_usuario",
                table: "TBL_DENUNCIAS",
                newName: "IX_TBL_DENUNCIAS_IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_DENUNCIAS_id_orgao",
                table: "TBL_DENUNCIAS",
                newName: "IX_TBL_DENUNCIAS_IdOrgaoPublico");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_DENUNCIAS_id_localizacao",
                table: "TBL_DENUNCIAS",
                newName: "IX_TBL_DENUNCIAS_IdLocalizacao");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_LOCALIZACAO_IdLocalizacao",
                table: "TBL_DENUNCIAS",
                column: "IdLocalizacao",
                principalTable: "TBL_LOCALIZACAO",
                principalColumn: "id_localizacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAOS_PUBLICOS_IdOrgaoPublico",
                table: "TBL_DENUNCIAS",
                column: "IdOrgaoPublico",
                principalTable: "TBL_ORGAOS_PUBLICOS",
                principalColumn: "id_orgao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_USUARIOS_IdUsuario",
                table: "TBL_DENUNCIAS",
                column: "IdUsuario",
                principalTable: "TBL_USUARIOS",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
