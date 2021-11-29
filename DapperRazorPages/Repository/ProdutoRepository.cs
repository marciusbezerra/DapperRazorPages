using Dapper;
using DapperRazorPages.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace DapperRazorPages.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            if (!File.Exists("data.db"))
                CreateDatabase();
        }

        private void CreateDatabase()
        {
            using (var cnn = new SQLiteConnection(GetConnection()))
            {
                cnn.Open();
                cnn.Execute(
                    @"
                    CREATE TABLE Produtos (
	                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
	                    Estoque DECIMAL(10,2) NOT NULL,
	                    Nome VARCHAR(100) NOT NULL,
	                    Preco DECIMAL(10,2) NOT NULL)
                ");
            }
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings")
                .GetSection("ProdutoConnection").Value;
            return connection;
        }

        public int Add(Produto produto)
        {
            var connectionString = GetConnection();
            int count = 0;
            using (var con = new SQLiteConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco); ";
                        //+ "SELECT CAST(SCOPE_IDENTITY() as INT);"; PARA SQL SERVER
                    count = con.Execute(query, produto);
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }

        public int Delete(int id)
        {
            var connectionString = GetConnection();
            int count = 0;
            using (var con = new SQLiteConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM Produtos WHERE Id =" + id;
                    count = con.Execute(query);
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }

        public int Edit(Produto produto)
        {
            var connectionString = GetConnection();
            var count = 0;
            using (var con = new SQLiteConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE Produtos SET Nome = @Nome, Estoque = @Estoque, Preco = @Preco " +
                        "WHERE Id = " + produto.Id;
                    count = con.Execute(query, produto);
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }

        public Produto Get(int id)
        {
            var connectionString = GetConnection();
            Produto produto = new Produto();
            using (var con = new SQLiteConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Produtos WHERE Id =" + id;
                    produto = con.Query<Produto>(query).FirstOrDefault();
                }
                finally
                {
                    con.Close();
                }
                return produto;
            }
        }

        public List<Produto> GetProdutos()
        {
            var connectionString = GetConnection();
            List<Produto> produtos = new List<Produto>();
            using (var con = new SQLiteConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Produtos";
                    produtos = con.Query<Produto>(query).ToList();
                }
                finally
                {
                    con.Close();
                }
                return produtos;
            }
        }
    }
}
