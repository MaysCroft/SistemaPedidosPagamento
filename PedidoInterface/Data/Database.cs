using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace PedidoInterface.Data
{
    public static class Database
    {
        // Define o caminho do banco de dados
        private static readonly string dbPath =
         Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pagamentowpf.db");

        static Database()
        {
            // Verificar se o banco de dados e a pasta existe
            var pastaBase = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(pastaBase))
                Directory.CreateDirectory(pastaBase);
            if (!File.Exists(dbPath))
                MessageBox.Show("Banco de dados inexistente!!!");
        }

        // Método que cria e abre uma conexão com o SQLite
        public static SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection($"Data Source={dbPath}");
            conn.Open();
            return conn;
        }

        // Inicializa o banco de dados criando tabelas e indices
        public static void Initialize()
        {
            using var conn = GetConnection();
            using var cmd = conn.CreateCommand();

            // Script do SQLite
            cmd.CommandText =
                """
                PRAGMA foreign_keys = ON;

                CREATE TABLE IF NOT EXISTS Pedidos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Data_Pedido TEXT NOT NULL,
                    Nome_Cliente TEXT NOT NULL,
                    Doc_Cliente TEXT NOT NULL,
                    Produto TEXT NOT NULL,
                    Quantidade INTEGER NOT NULL,
                    Valor REAL NOT NULL,
                    StatusPedido TEXT NOT NULL,
                    FormaPagamento TEXT NOT NULL,
                    StatusPagamento TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS __EFMigrationsHistory (
                    MigrationId TEXT PRIMARY KEY,
                    ProductVersion TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS __EFMigrationsLock (
                    Id INTEGER PRIMARY KEY,
                    MigrationId TEXT NOT NULL,
                    ProductVersion TEXT NOT NULL
                );
                """;

            cmd.ExecuteNonQuery();
        }
    }
}