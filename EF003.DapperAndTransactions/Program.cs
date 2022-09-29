using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Transactions;

namespace EF003.DapperAndTransactions
{
    class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IDbConnection db = new SqlConnection(configuration.GetSection("constr").Value);

            // Transfer 2000
            // From: 8   Sarah   10000
            // To:  4   Menna   5500

            decimal amountToTranfer = 2000m;

            using (var transactionScope = new TransactionScope())
            {
                // sarah
                var walletFrom = db.QuerySingle<Wallet>
                  ("SELECT * FROM Wallets Where Id = @Id", new {Id = 8});
            
                // Menna
                var walletTo = db.QuerySingle<Wallet>
                  ("SELECT * FROM Wallets Where Id = @Id", new { Id = 4 }); 

                db.Execute("UPDATE Wallets Set Balance = @Balance Where Id = @Id",
                    new
                    {
                        Id = walletFrom.Id,
                        Balance = walletFrom.Balance - amountToTranfer
                    }
                ); ;

                db.Execute("UPDATE Wallets Set Balance = @Balance Where Id = @Id",
                  new
                  {
                      Id = walletTo.Id,
                      Balance = walletTo.Balance + amountToTranfer
                  }
                );
                transactionScope.Complete();

            }

            Console.ReadKey();
        }
    }
}