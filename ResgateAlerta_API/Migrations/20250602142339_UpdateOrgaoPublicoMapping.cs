using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoDenuncia.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrgaoPublicoMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAO_PUBLICO_IdOrgaoPublico",
                table: "TBL_DENUNCIAS");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAO_PUBLICO_OrgaoPublicoIdOrgaoPublico",
                table: "TBL_DENUNCIAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBL_ORGAO_PUBLICO",
                table: "TBL_ORGAO_PUBLICO");

            migrationBuilder.RenameTable(
                name: "TBL_ORGAO_PUBLICO",
                newName: "TBL_ORGAOS_PUBLICOS");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TBL_ORGAOS_PUBLICOS",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "AreaAtuacao",
                table: "TBL_ORGAOS_PUBLICOS",
                newName: "area_atuacao");

            migrationBuilder.RenameColumn(
                name: "IdOrgaoPublico",
                table: "TBL_ORGAOS_PUBLICOS",
                newName: "id_orgao");

            migrationBuilder.AlterColumn<string>(
                name: "area_atuacao",
                table: "TBL_ORGAOS_PUBLICOS",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(8)",
                oldMaxLength: 8);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBL_ORGAOS_PUBLICOS",
                table: "TBL_ORGAOS_PUBLICOS",
                column: "id_orgao");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAOS_PUBLICOS_IdOrgaoPublico",
                table: "TBL_DENUNCIAS",
                column: "IdOrgaoPublico",
                principalTable: "TBL_ORGAOS_PUBLICOS",
                principalColumn: "id_orgao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAOS_PUBLICOS_OrgaoPublicoIdOrgaoPublico",
                table: "TBL_DENUNCIAS",
                column: "OrgaoPublicoIdOrgaoPublico",
                principalTable: "TBL_ORGAOS_PUBLICOS",
                principalColumn: "id_orgao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAOS_PUBLICOS_IdOrgaoPublico",
                table: "TBL_DENUNCIAS");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAOS_PUBLICOS_OrgaoPublicoIdOrgaoPublico",
                table: "TBL_DENUNCIAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBL_ORGAOS_PUBLICOS",
                table: "TBL_ORGAOS_PUBLICOS");

            migrationBuilder.RenameTable(
                name: "TBL_ORGAOS_PUBLICOS",
                newName: "TBL_ORGAO_PUBLICO");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "TBL_ORGAO_PUBLICO",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "area_atuacao",
                table: "TBL_ORGAO_PUBLICO",
                newName: "AreaAtuacao");

            migrationBuilder.RenameColumn(
                name: "id_orgao",
                table: "TBL_ORGAO_PUBLICO",
                newName: "IdOrgaoPublico");

            migrationBuilder.AlterColumn<string>(
                name: "AreaAtuacao",
                table: "TBL_ORGAO_PUBLICO",
                type: "NVARCHAR2(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBL_ORGAO_PUBLICO",
                table: "TBL_ORGAO_PUBLICO",
                column: "IdOrgaoPublico");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAO_PUBLICO_IdOrgaoPublico",
                table: "TBL_DENUNCIAS",
                column: "IdOrgaoPublico",
                principalTable: "TBL_ORGAO_PUBLICO",
                principalColumn: "IdOrgaoPublico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_DENUNCIAS_TBL_ORGAO_PUBLICO_OrgaoPublicoIdOrgaoPublico",
                table: "TBL_DENUNCIAS",
                column: "OrgaoPublicoIdOrgaoPublico",
                principalTable: "TBL_ORGAO_PUBLICO",
                principalColumn: "IdOrgaoPublico");
        }
    }
}
