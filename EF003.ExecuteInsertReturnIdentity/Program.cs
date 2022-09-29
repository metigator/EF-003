using EF003.ExecuteInsertReturnIdentity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using Dapper;
using System.Linq;

namespace EF003.ExecuteInsertReturnIdentity
{
    class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IDbConnection db = new SqlConnection(configuration.GetSection("constr").Value);

            var walletToInsert = new Wallet { Holder = "Ayman", Balance = 16000m };

            var sql = "INSERT INTO Wallets (Holder, Balance) " +
                      "VALUES (@Holder, @Balance)" +
                      "SELECT CAST(SCOPE_IDENTITY() AS INT)";


            var parameters =
                new
                {
                    Holder = walletToInsert.Holder,
                    Balance = walletToInsert.Balance
                };


            walletToInsert.Id = db.Query<int>(sql, parameters).Single();


            Console.WriteLine(walletToInsert);

            Console.ReadKey();
        } 
    }
}