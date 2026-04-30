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
        private static readonly string pastaBase =
        Path.Combine(@"C:\Users\midor\OneDrive\Documentos",
            "AppOrganizacaoPessoal");

        private static readonly string caminhoBanco =
            Path.Combine(pastaBase, "tarefas.db");

        private static readonly string connectionString =
            $"Data Source = {caminhoBanco}";

        static Database()
        {
            // Verificar se o banco de dados e a pasta existe
            if (!Directory.Exists(pastaBase))
                Directory.CreateDirectory(pastaBase);
            if (!File.Exists(connectionString))
                MessageBox.Show("Banco de dados inexistente!!!");
        }

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }
    }
}
