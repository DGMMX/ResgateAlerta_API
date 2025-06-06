using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoDenuncia.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEstadoMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_ESTADO",
                columns: table => new
                {
                    id_estado = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    uf = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ESTADO", x => x.id_estado);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ORGAO_PUBLICO",
                columns: table => new
                {
                    IdOrgaoPublico = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    AreaAtuacao = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ORGAO_PUBLICO", x => x.IdOrgaoPublico);
                });

            migrationBuilder.CreateTable(
                name: "TBL_USUARIOS",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    TipoUsuario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_USUARIOS", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "TBL_CIDADE",
                columns: table => new
                {
                    IdCidade = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    IdEstado = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CIDADE", x => x.IdCidade);
                    table.ForeignKey(
                        name: "FK_TBL_CIDADE_TBL_ESTADO_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "TBL_ESTADO",
                        principalColumn: "id_estado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_BAIRRO",
                columns: table => new
                {
                    IdBairro = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    IdCidade = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_BAIRRO", x => x.IdBairro);
                    table.ForeignKey(
                        name: "FK_TBL_BAIRRO_TBL_CIDADE_IdCidade",
                        column: x => x.IdCidade,
                        principalTable: "TBL_CIDADE",
                        principalColumn: "IdCidade",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_LOCALIZACAO",
                columns: table => new
                {
                    IdLocalização = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Logradouro = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Numero = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    Complemento = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Cep = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    IdBairro = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_LOCALIZACAO", x => x.IdLocalização);
                    table.ForeignKey(
                        name: "FK_TBL_LOCALIZACAO_TBL_BAIRRO_IdBairro",
                        column: x => x.IdBairro,
                        principalTable: "TBL_BAIRRO",
                        principalColumn: "IdBairro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DENUNCIAS",
                columns: table => new
                {
                    IdDenuncia = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    IdLocalizacao = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    IdOrgaoPublico = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    LocalizacaoIdLocalização = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    OrgaoPublicoIdOrgaoPublico = table.Column<Guid>(type: "RAW(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_DENUNCIAS", x => x.IdDenuncia);
                    table.ForeignKey(
                        name: "FK_TBL_DENUNCIAS_TBL_LOCALIZACAO_IdLocalizacao",
                        column: x => x.IdLocalizacao,
                        principalTable: "TBL_LOCALIZACAO",
                        principalColumn: "IdLocalização",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_DENUNCIAS_TBL_LOCALIZACAO_LocalizacaoIdLocalização",
                        column: x => x.LocalizacaoIdLocalização,
                        principalTable: "TBL_LOCALIZACAO",
                        principalColumn: "IdLocalização");
                    table.ForeignKey(
                        name: "FK_TBL_DENUNCIAS_TBL_ORGAO_PUBLICO_IdOrgaoPublico",
                        column: x => x.IdOrgaoPublico,
                        principalTable: "TBL_ORGAO_PUBLICO",
                        principalColumn: "IdOrgaoPublico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_DENUNCIAS_TBL_ORGAO_PUBLICO_OrgaoPublicoIdOrgaoPublico",
                        column: x => x.OrgaoPublicoIdOrgaoPublico,
                        principalTable: "TBL_ORGAO_PUBLICO",
                        principalColumn: "IdOrgaoPublico");
                    table.ForeignKey(
                        name: "FK_TBL_DENUNCIAS_TBL_USUARIOS_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                columns: table => new
                {
                    IdAcompanhamento = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Observacao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    IdDenuncia = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ACOMPANHAMENTO_DENUNCIAS", x => x.IdAcompanhamento);
                    table.ForeignKey(
                        name: "FK_TBL_ACOMPANHAMENTO_DENUNCIAS_TBL_DENUNCIAS_IdDenuncia",
                        column: x => x.IdDenuncia,
                        principalTable: "TBL_DENUNCIAS",
                        principalColumn: "IdDenuncia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ACOMPANHAMENTO_DENUNCIAS_IdDenuncia",
                table: "TBL_ACOMPANHAMENTO_DENUNCIAS",
                column: "IdDenuncia");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_BAIRRO_IdCidade",
                table: "TBL_BAIRRO",
                column: "IdCidade");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_CIDADE_IdEstado",
                table: "TBL_CIDADE",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DENUNCIAS_IdLocalizacao",
                table: "TBL_DENUNCIAS",
                column: "IdLocalizacao");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DENUNCIAS_IdOrgaoPublico",
                table: "TBL_DENUNCIAS",
                column: "IdOrgaoPublico");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DENUNCIAS_IdUsuario",
                table: "TBL_DENUNCIAS",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DENUNCIAS_LocalizacaoIdLocalização",
                table: "TBL_DENUNCIAS",
                column: "LocalizacaoIdLocalização");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DENUNCIAS_OrgaoPublicoIdOrgaoPublico",
                table: "TBL_DENUNCIAS",
                column: "OrgaoPublicoIdOrgaoPublico");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_LOCALIZACAO_IdBairro",
                table: "TBL_LOCALIZACAO",
                column: "IdBairro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_ACOMPANHAMENTO_DENUNCIAS");

            migrationBuilder.DropTable(
                name: "TBL_DENUNCIAS");

            migrationBuilder.DropTable(
                name: "TBL_LOCALIZACAO");

            migrationBuilder.DropTable(
                name: "TBL_ORGAO_PUBLICO");

            migrationBuilder.DropTable(
                name: "TBL_USUARIOS");

            migrationBuilder.DropTable(
                name: "TBL_BAIRRO");

            migrationBuilder.DropTable(
                name: "TBL_CIDADE");

            migrationBuilder.DropTable(
                name: "TBL_ESTADO");
        }
    }
}
