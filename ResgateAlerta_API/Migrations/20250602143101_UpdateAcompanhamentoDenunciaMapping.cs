using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoDenuncia.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAcompanhamentoDenunciaMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_ACOMPANHAMENTO_DENUNCIAS_TBL_DENUNCIAS_IdDenuncia",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBL_ACOMPANHAMENTO_DENUNCIAS",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS");

            migrationBuilder.RenameTable(
                name: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                newName: "TBL_ACOMPANHAMENTO_DENUNCIA");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TBL_ACOMPANHAMENTO_DENUNCIA",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Observacao",
                table: "TBL_ACOMPANHAMENTO_DENUNCIA",
                newName: "observacao");

            migrationBuilder.RenameColumn(
                name: "IdDenuncia",
                table: "TBL_ACOMPANHAMENTO_DENUNCIA",
                newName: "id_denuncia");

            migrationBuilder.RenameColumn(
                name: "DataAtualizacao",
                table: "TBL_ACOMPANHAMENTO_DENUNCIA",
                newName: "data_atualizacao");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_ACOMPANHAMENTO_DENUNCIAS_IdDenuncia",
                table: "TBL_ACOMPANHAMENTO_DENUNCIA",
                newName: "IX_TBL_ACOMPANHAMENTO_DENUNCIA_id_denuncia");

            migrationBuilder.AlterColumn<string>(
                name: "observacao",
                table: "TBL_ACOMPANHAMENTO_DENUNCIA",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(500)",
                oldMaxLength: 500);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBL_ACOMPANHAMENTO_DENUNCIA",
                table: "TBL_ACOMPANHAMENTO_DENUNCIA",
                column: "IdAcompanhamento");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_ACOMPANHAMENTO_DENUNCIA_TBL_DENUNCIAS_id_denuncia",
                table: "TBL_ACOMPANHAMENTO_DENUNCIA",
                column: "id_denuncia",
                principalTable: "TBL_DENUNCIAS",
                principalColumn: "id_denuncia",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_ACOMPANHAMENTO_DENUNCIA_TBL_DENUNCIAS_id_denuncia",
                table: "TBL_ACOMPANHAMENTO_DENUNCIA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBL_ACOMPANHAMENTO_DENUNCIA",
                table: "TBL_ACOMPANHAMENTO_DENUNCIA");

            migrationBuilder.RenameTable(
                name: "TBL_ACOMPANHAMENTO_DENUNCIA",
                newName: "TBL_ACOMPANHAMENTO_DENUNCIAS");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "observacao",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                newName: "Observacao");

            migrationBuilder.RenameColumn(
                name: "id_denuncia",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                newName: "IdDenuncia");

            migrationBuilder.RenameColumn(
                name: "data_atualizacao",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                newName: "DataAtualizacao");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_ACOMPANHAMENTO_DENUNCIA_id_denuncia",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                newName: "IX_TBL_ACOMPANHAMENTO_DENUNCIAS_IdDenuncia");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                type: "NVARCHAR2(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBL_ACOMPANHAMENTO_DENUNCIAS",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                column: "IdAcompanhamento");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_ACOMPANHAMENTO_DENUNCIAS_TBL_DENUNCIAS_IdDenuncia",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                column: "IdDenuncia",
                principalTable: "TBL_DENUNCIAS",
                principalColumn: "id_denuncia",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
