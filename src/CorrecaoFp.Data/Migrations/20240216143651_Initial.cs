using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CorrecaoFp.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Capacitor",
                columns: table => new
                {
                    Capacitor_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Capacitor_Fabricante = table.Column<string>(type: "TEXT", nullable: true),
                    Capacitor_Potencia = table.Column<double>(type: "REAL", nullable: false),
                    Capacitor_Tensao = table.Column<double>(type: "REAL", nullable: false),
                    Capacitor_Unidade = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capacitor", x => x.Capacitor_ID);
                });

            migrationBuilder.CreateTable(
                name: "Configuracao",
                columns: table => new
                {
                    Configuracao_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Configuracao_Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Configuracao_Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Configuracao_Valor = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracao", x => x.Configuracao_ID);
                });

            migrationBuilder.CreateTable(
                name: "Medicao",
                columns: table => new
                {
                    Medicao_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Medicao_DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Medicao_DataFim = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Medicao_PotenciaAtiva = table.Column<double>(type: "REAL", nullable: false),
                    Medicao_PotenciaReativa = table.Column<double>(type: "REAL", nullable: false),
                    Medicao_PotenciaAparente = table.Column<double>(type: "REAL", nullable: false),
                    Medicao_FatorPotencia = table.Column<double>(type: "REAL", nullable: false),
                    Medicao_TipoFatorPotencia = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicao", x => new { x.Medicao_ID, x.Medicao_DataInicio });
                });

            migrationBuilder.CreateTable(
                name: "Modo_Compensacao",
                columns: table => new
                {
                    Modo_Compensacao_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Modo_Compensacao_Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modo_Compensacao", x => x.Modo_Compensacao_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Potencia",
                columns: table => new
                {
                    Tipo_Potencia_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tipo_Potencia_Sigla = table.Column<string>(type: "TEXT", nullable: true),
                    Tipo_Potencia_Descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Potencia", x => x.Tipo_Potencia_ID);
                });

            migrationBuilder.CreateTable(
                name: "Estagio",
                columns: table => new
                {
                    Estagio_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tipo_Potencia_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Capacitor_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Estagio_Descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estagio", x => x.Estagio_ID);
                    table.ForeignKey(
                        name: "FK_Estagio_Capacitor_Capacitor_ID",
                        column: x => x.Capacitor_ID,
                        principalTable: "Capacitor",
                        principalColumn: "Capacitor_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estagio_Tipo_Potencia_Tipo_Potencia_ID",
                        column: x => x.Tipo_Potencia_ID,
                        principalTable: "Tipo_Potencia",
                        principalColumn: "Tipo_Potencia_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 1, "WEG", 0.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 17, "WEG", 30.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 16, "WEG", 25.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 14, "WEG", 17.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 13, "WEG", 15.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 12, "WEG", 12.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 11, "WEG", 10.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 10, "WEG", 7.5, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 15, "WEG", 20.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 8, "WEG", 3.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 7, "WEG", 2.5, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 6, "WEG", 2.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 5, "WEG", 1.5, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 4, "WEG", 1.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 3, "WEG", 0.75, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 2, "WEG", 0.5, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Capacitor",
                columns: new[] { "Capacitor_ID", "Capacitor_Fabricante", "Capacitor_Potencia", "Capacitor_Tensao", "Capacitor_Unidade" },
                values: new object[] { 9, "WEG", 5.0, 220.0, "kVAr" });

            migrationBuilder.InsertData(
                table: "Configuracao",
                columns: new[] { "Configuracao_ID", "Configuracao_Descricao", "Configuracao_Nome", "Configuracao_Valor" },
                values: new object[] { 4, "Relação de transformação do transformador de corrente (TC)", "RELACAO_TC", 800.0 });

            migrationBuilder.InsertData(
                table: "Configuracao",
                columns: new[] { "Configuracao_ID", "Configuracao_Descricao", "Configuracao_Nome", "Configuracao_Valor" },
                values: new object[] { 3, "Tensão de linha (V)", "TENSAO_LINHA", 127.0 });

            migrationBuilder.InsertData(
                table: "Configuracao",
                columns: new[] { "Configuracao_ID", "Configuracao_Descricao", "Configuracao_Nome", "Configuracao_Valor" },
                values: new object[] { 1, "Quantidade de estágios fixos", "QUANTIDADE_ESTAGIOS_FIXOS", 4.0 });

            migrationBuilder.InsertData(
                table: "Configuracao",
                columns: new[] { "Configuracao_ID", "Configuracao_Descricao", "Configuracao_Nome", "Configuracao_Valor" },
                values: new object[] { 2, "Quantidade de estágios automáticos", "QUANTIDADE_ESTAGIOS_AUTOMATICOS", 12.0 });

            migrationBuilder.InsertData(
                table: "Medicao",
                columns: new[] { "Medicao_DataInicio", "Medicao_ID", "Medicao_DataFim", "Medicao_FatorPotencia", "Medicao_PotenciaAparente", "Medicao_PotenciaAtiva", "Medicao_PotenciaReativa", "Medicao_TipoFatorPotencia" },
                values: new object[] { new DateTime(2023, 6, 1, 8, 15, 10, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 6, 1, 8, 30, 10, 0, DateTimeKind.Unspecified), 0.96999999999999997, 14769.799999999999, 14326.780000000001, -2881.5799999999999, "Cap" });

            migrationBuilder.InsertData(
                table: "Modo_Compensacao",
                columns: new[] { "Modo_Compensacao_ID", "Modo_Compensacao_Nome" },
                values: new object[] { 1, "PFW03-M12 (Modo inteligente)" });

            migrationBuilder.InsertData(
                table: "Modo_Compensacao",
                columns: new[] { "Modo_Compensacao_ID", "Modo_Compensacao_Nome" },
                values: new object[] { 2, "Sequencial Ascendente" });

            migrationBuilder.InsertData(
                table: "Modo_Compensacao",
                columns: new[] { "Modo_Compensacao_ID", "Modo_Compensacao_Nome" },
                values: new object[] { 3, "Sequencial Descendente" });

            migrationBuilder.InsertData(
                table: "Modo_Compensacao",
                columns: new[] { "Modo_Compensacao_ID", "Modo_Compensacao_Nome" },
                values: new object[] { 4, "Linear" });

            migrationBuilder.InsertData(
                table: "Modo_Compensacao",
                columns: new[] { "Modo_Compensacao_ID", "Modo_Compensacao_Nome" },
                values: new object[] { 5, "Circular" });

            migrationBuilder.InsertData(
                table: "Modo_Compensacao",
                columns: new[] { "Modo_Compensacao_ID", "Modo_Compensacao_Nome" },
                values: new object[] { 6, "Manual" });

            migrationBuilder.InsertData(
                table: "Tipo_Potencia",
                columns: new[] { "Tipo_Potencia_ID", "Tipo_Potencia_Descricao", "Tipo_Potencia_Sigla" },
                values: new object[] { 1, "Capacitor Trifásico", "C" });

            migrationBuilder.InsertData(
                table: "Tipo_Potencia",
                columns: new[] { "Tipo_Potencia_ID", "Tipo_Potencia_Descricao", "Tipo_Potencia_Sigla" },
                values: new object[] { 2, "Reator de Derivação Trifásico", "L" });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 1, 1, "Potência do estárgio 1", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 26, 13, "Potência do estágio fixo 2", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 25, 12, "Potência do estágio fixo 1", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 24, 1, "Potência do estárgio 24", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 23, 1, "Potência do estárgio 23", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 22, 1, "Potência do estárgio 22", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 21, 1, "Potência do estárgio 21", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 20, 1, "Potência do estárgio 20", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 19, 1, "Potência do estárgio 19", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 18, 1, "Potência do estárgio 18", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 17, 17, "Potência do estárgio 17", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 16, 16, "Potência do estárgio 16", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 15, 15, "Potência do estárgio 15", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 14, 14, "Potência do estárgio 14", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 13, 13, "Potência do estárgio 13", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 12, 12, "Potência do estárgio 12", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 11, 11, "Potência do estárgio 11", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 10, 10, "Potência do estárgio 10", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 9, 9, "Potência do estárgio 9", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 8, 8, "Potência do estárgio 8", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 7, 7, "Potência do estárgio 7", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 6, 6, "Potência do estárgio 6", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 5, 5, "Potência do estárgio 5", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 4, 4, "Potência do estárgio 4", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 3, 3, "Potência do estárgio 3", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 2, 2, "Potência do estárgio 2", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 27, 13, "Potência do estágio fixo 3", 1 });

            migrationBuilder.InsertData(
                table: "Estagio",
                columns: new[] { "Estagio_ID", "Capacitor_ID", "Estagio_Descricao", "Tipo_Potencia_ID" },
                values: new object[] { 28, 13, "Potência do estágio fixo 4", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Estagio_Capacitor_ID",
                table: "Estagio",
                column: "Capacitor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Estagio_Tipo_Potencia_ID",
                table: "Estagio",
                column: "Tipo_Potencia_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuracao");

            migrationBuilder.DropTable(
                name: "Estagio");

            migrationBuilder.DropTable(
                name: "Medicao");

            migrationBuilder.DropTable(
                name: "Modo_Compensacao");

            migrationBuilder.DropTable(
                name: "Capacitor");

            migrationBuilder.DropTable(
                name: "Tipo_Potencia");
        }
    }
}
