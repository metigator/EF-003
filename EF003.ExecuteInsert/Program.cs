using EF003.ExecuteInsert;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using Dapper;
using System.Linq;

namespace EF003.ExecuteInsert
{
    class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IDbConnection db = new SqlConnection(configuration.GetSection("constr").Value);

            var walletToInsert = new Wallet { Holder = "Sarah", Balance = 10000m };

            var sql = "INSERT INTO Wallets (Holder, Balance) " +
                      "VALUES (@Holder, @Balance)";

            db.Execute(sql,
                new { 
                    Holder = walletToInsert.Holder, 
                    Balance = walletToInsert.Balance 
                });

            Console.ReadKey();
        } 
    }
}