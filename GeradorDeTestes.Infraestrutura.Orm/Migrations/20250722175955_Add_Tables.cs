using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeradorDeTestes.Infraestrutura.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disciplina",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisciplinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Serie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materias_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Enunciado = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MateriasId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FoiAcertada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questoes_Materias_MateriasId",
                        column: x => x.MateriasId,
                        principalTable: "Materias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teste",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisciplinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Serie = table.Column<int>(type: "int", nullable: false),
                    QuantidadeQuestoes = table.Column<int>(type: "int", nullable: false),
                    TipoTeste = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teste_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teste_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Alternativas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correta = table.Column<bool>(type: "bit", nullable: false),
                    QuestaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternativas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBAlternativa_TBQuestao",
                        column: x => x.QuestaoId,
                        principalTable: "Questoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternativas_QuestaoId",
                table: "Alternativas",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_DisciplinaId",
                table: "Materias",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Questoes_MateriasId",
                table: "Questoes",
                column: "MateriasId");

            migrationBuilder.CreateIndex(
                name: "IX_Teste_DisciplinaId",
                table: "Teste",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Teste_MateriaId",
                table: "Teste",
                column: "MateriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alternativas");

            migrationBuilder.DropTable(
                name: "Teste");

            migrationBuilder.DropTable(
                name: "Questoes");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Disciplina");
        }
    }
}
