using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using ExecutionsClass;
using Executions = ExecutionsClass.Executions;
using Query = ListExecutionQueryClass.ListExecutionQuery;

namespace SqLiteExecutionsToListAndQueryResults
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = @"Data Source = C:\data\NinjaTrader.sqlite";
                List<Executions> listExecution = new List<Executions>();
                List<Query> selectedList = new List<Query>();
                List<Query> listFromQuery = new List<Query>();

            var symbol = "NQ";
            var instrument = 699839150758595;


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

                //  used to hold data after read from db

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
                try
                {

                    // 	cycle through listExecution and retreive needed variables
                    foreach (var execList in listExecution)
                    {
                        //	create ListExecutionQueryClass
                        Query list = new Query();

                        //	fill new list 
                        list.Id = execList.Id;
                        list.Symbol = symbol;
                        list.Instrument = execList.Instrument;
                        list.IsEntry = execList.IsEntry;
                        list.IsExit = execList.IsExit;
                        list.Position = execList.Position;
                        list.Quantity = execList.Quantity;
                        list.Price = execList.Price;
                        list.Time = execList.Time;

                        //	add to list
                        selectedList.Add(list);

                    }

                    //selectedList.Dump("selectedList");

                }

                catch
                {
                    Console.WriteLine("error in foreach");
                }
                try
                {
                    //	use query to create list
                    var query = (from l in selectedList
                                 where (Int64)l.Instrument == (Int64)62124056207858786
                                 select new Query()
                                 {
                                     Id = l.Id,
                                     Symbol = symbol,
                                     Instrument = l.Instrument,
                                     IsEntry = l.IsEntry,
                                     IsExit = l.IsExit,
                                     Position = l.Position,
                                     Quantity = l.Quantity,
                                     Price = l.Price,
                                     Time = l.Time,
                                     HumanTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime((long)l.Time), TimeZoneInfo.Local).ToString("  HH:mm:ss MM/dd/yyyy  ")

                                     //HumanTime = new DateTime((long)l.Time)
                                 }).ToList();
                    //query.Dump("query");
                    listFromQuery = query.ToList();
                }

                catch
                {
                    Console.WriteLine("Query");
                }
            }
        }
    }
}
