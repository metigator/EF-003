using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using Dapper;
using System.Linq;

namespace EF003.ExecuteUpdate
{
    class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IDbConnection db = new SqlConnection(configuration.GetSection("constr").Value);

            var walletToUpdate = new Wallet { Id = 9, Holder = "Ammar"
                , Balance = 9000m };

            var sql = "UPDATE Wallets SET Holder = @Holder , Balance = @Balance " +
                      "WHERE Id = @Id;";


            var parameters =
                new
                {
                    Id = walletToUpdate.Id,
                    Holder = walletToUpdate.Holder,
                    Balance = walletToUpdate.Balance
                };

            db.Execute(sql, parameters);

            Console.ReadKey();
        } 
    }
}