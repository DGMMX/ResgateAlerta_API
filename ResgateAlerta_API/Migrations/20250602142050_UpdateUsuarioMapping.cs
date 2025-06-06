using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoDenuncia.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsuarioMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "TBL_USUARIOS",
                newName: "senha");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TBL_USUARIOS",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "TBL_USUARIOS",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "TipoUsuario",
                table: "TBL_USUARIOS",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "TBL_USUARIOS",
                newName: "id_usuario");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "TBL_USUARIOS",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senha",
                table: "TBL_USUARIOS",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "TBL_USUARIOS",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "TBL_USUARIOS",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "TBL_USUARIOS",
                newName: "TipoUsuario");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "TBL_USUARIOS",
                newName: "IdUsuario");

            migrationBuilder.AlterColumn<string>(
                name: "TipoUsuario",
                table: "TBL_USUARIOS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20);
        }
    }
}
