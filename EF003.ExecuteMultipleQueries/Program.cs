using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;

namespace EF003.ExecuteMultipleQueries
{
    class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IDbConnection db = new SqlConnection(configuration.GetSection("constr").Value);

            var sql = "SELECT MIN(Balance) FROM Wallets;" +
                       "SELECT MAX(Balance) FROM Wallets;";

            var multi = db.QueryMultiple(sql);


            //Console.WriteLine(
            //    $"Min = {multi.ReadSingle<decimal>()}" +
            //    $"\nMax = {multi.ReadSingle<decimal>()}");


            Console.WriteLine(
              $"Min = {multi.Read<decimal>().Single()}" +
              $"\nMax = {multi.Read<decimal>().Single()}");

            Console.ReadKey();
        }
    }
}