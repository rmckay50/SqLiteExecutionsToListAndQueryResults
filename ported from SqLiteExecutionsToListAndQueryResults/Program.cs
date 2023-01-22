using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;

namespace SqLiteExecutionsToListAndQueryResults
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = @"Data Source = C:\data\NinjaTrader.sqlite";

            using (var db = new System.Data.SQLite.SQLiteConnection(path))
            {
                try
                {
                    db.Open();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in opening db");
                }

            }
        }
    }
}
