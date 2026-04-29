using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPagamento.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data_Pedido = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nome_Cliente = table.Column<string>(type: "TEXT", nullable: false),
                    Doc_Cliente = table.Column<string>(type: "TEXT", nullable: false),
                    Produto = table.Column<string>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Valor = table.Column<double>(type: "REAL", nullable: false),
                    StatusPedido = table.Column<string>(type: "TEXT", nullable: false),
                    FormaPagamento = table.Column<string>(type: "TEXT", nullable: false),
                    StatusPagamento = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamentos");
        }
    }
}
