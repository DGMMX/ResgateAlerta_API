using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoDenuncia.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCidadeMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_CIDADE_TBL_ESTADO_IdEstado",
                table: "TBL_CIDADE");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TBL_CIDADE",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "IdEstado",
                table: "TBL_CIDADE",
                newName: "id_estado");

            migrationBuilder.RenameColumn(
                name: "IdCidade",
                table: "TBL_CIDADE",
                newName: "id_cidade");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_CIDADE_IdEstado",
                table: "TBL_CIDADE",
                newName: "IX_TBL_CIDADE_id_estado");

            migrationBuilder.AddForeignKey(
                name: "fk_estado",
                table: "TBL_CIDADE",
                column: "id_estado",
                principalTable: "TBL_ESTADO",
                principalColumn: "id_estado",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_estado",
                table: "TBL_CIDADE");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "TBL_CIDADE",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "id_estado",
                table: "TBL_CIDADE",
                newName: "IdEstado");

            migrationBuilder.RenameColumn(
                name: "id_cidade",
                table: "TBL_CIDADE",
                newName: "IdCidade");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_CIDADE_id_estado",
                table: "TBL_CIDADE",
                newName: "IX_TBL_CIDADE_IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_CIDADE_TBL_ESTADO_IdEstado",
                table: "TBL_CIDADE",
                column: "IdEstado",
                principalTable: "TBL_ESTADO",
                principalColumn: "id_estado",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
