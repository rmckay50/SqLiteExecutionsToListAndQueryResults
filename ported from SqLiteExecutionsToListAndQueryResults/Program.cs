using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using ExecutionsClass;
using Executions = ExecutionsClass.Executions;
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
                ///<summary>
                ///<param> create reader, command </param>
                ///<
                /// </summary>
                /// 

                SQLiteDataReader reader;
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = db.CreateCommand();
                sqlite_cmd.CommandText = "SELECT * FROM Executions";

                reader = sqlite_cmd.ExecuteReader();
                List<Executions> listExecution = new List<Executions>();
                    while (reader.Read())
                    {
                        Executions exec = new Executions();
                        exec.Id                     = (long)reader[0];
                        exec.Account                = (long)reader[1];
                        exec.BarIndex               = (long)reader[2];
                        exec.Commission             = (System.Double)reader[3];
                        exec.Exchange               = (long)reader[4];
                        exec.ExecutionId            = (string)reader[5];
                        exec.Fee                    = (double)reader[6];
                        exec.Instrument             = (long)reader[7];
                        exec.IsEntry                = (long)reader[8];
                        exec.IsEntryStrategy        = (long)reader[9];
                        exec.IsExit                 = (long)reader[10];
                        exec.IsExitStrategy         = (long)reader[11];
                        exec.LotSize                = (long)reader[12];
                        exec.MarketPosition         = (long)reader[13];
                        exec.MaxPrice               = (System.Double)reader[14];
                        exec.MinPrice               = (System.Double)reader[15];
                        exec.Name                   = (string)reader[16];
                        exec.OrderId                = (string)reader[17];
                        exec.Position               = (long)reader[18];
                        exec.PositionStrategy       = (long)reader[19];   
                        exec.Price                  = (System.Double)reader[20];
                        exec.Quantity               = (long)reader[21];
                        exec.Rate                   = (double)reader[22];
                        exec.StatementDate          = (long)reader[23];
                        exec.Time                   = (long)reader[24];
                        exec.ServerName             = (string)reader[25];

                        //  add row to list
                        listExecution.Add(exec);
                        #region In work
                        //var query = from e in listExecution
                        //			where e.Id > 16621
                        //			select e;
                        //	query = query.ToList().Dump();
                        /*
                        try
                        {
                            db.Open();
                        }
                        catch (Exception)
                        { }
                        string filePath = @"C:\Error.txt";
                        SQLiteDataReader reader;
                        Console.WriteLine(string.Format("in catch"));
                        SQLiteCommand sqlite_cmd;
                        sqlite_cmd = db.CreateCommand();
                        sqlite_cmd.CommandText = "SELECT Id, Account FROM Executions";
                        reader = sqlite_cmd.ExecuteReader();





                        List<string> id = new List<string>();
                        while (reader.Read())
                        {
                            string myreader = reader.GetInt64(0).ToString();
                                reader.GetInt64(0).ToString();
                            id.Add(myreader);
                            Console.WriteLine(myreader);
                        }
                        //id = id.ToList().Dump();
                var query = from ex in id 
                select ex;
                query = query.ToList().Take(10).Dump();
                */
                        #endregion In work
                    }
                    //listExecution.Dump();



                    db.Close();


                }
                catch (Exception)
                {
                    Console.WriteLine("Error in opening db");
                }

            }
        }
    }
}
