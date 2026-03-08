using Microsoft.EntityFrameworkCore.Migrations;

namespace CorrecaoFp.Data.Migrations
{
    public partial class AlterEstagio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Estagio",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Estagio");
        }
    }
}
