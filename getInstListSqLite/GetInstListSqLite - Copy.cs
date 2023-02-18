using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using ExecutionsClass;
using Executions = ExecutionsClass.Executions;
using Ret;
//using Query = ListExecutionQueryClass.ListExecutionQuery;
//using SqLiteExecutionsToListAndQueryResults;





namespace GetInstListSqLite
{
    public static class Methods
    {
        //public static List<Ret> instList = new List<Ret>();
        public static List<Ret.Ret> getInstList(string name,
            string startDate, string endDate, bool bPlayback, string pathFromCall)
        {
            //var path = @"Data Source = C:\Users\Owner\Documents\NinjaTrader 8\db\NinjaTrader.sqlite";
            var path = pathFromCall;
            //  list to hold valiables in Executions table from NinjaTrader.sqlite
            List<Executions> listExecution = new List<Executions>();
            //  list to hold Ret() format from listExecution
            List<Ret.Ret> listExecutionRet = new List<Ret.Ret>();
            //List<Query> selectedList = new List<Query>();
            //List<Ret.Ret> instList = new List<Ret.Ret>();
           var instList = new List<Ret.Ret>();

            //List<Query> listFromQuery = new List<Query>();

            //  Below is code from getInstList for filling in Expiry
            //  Expiry is located in Instruments 
            //  Can either load Instruments and do query or get info from chart 
            //  Will try char and in interim just add string 'Dec 2022'
            //  Format from .sdf instList was 'Dec 2022' 
            //  Expiry = new DateTime((long)mInsIns.Expiry).ToString(" MMM yyyy"),
            //  public string Expiry { get; set; }
            var symbol = "NQ";
            var instrument = 699839150758595;



            // Convert dates to Utc ticks
            // Used for start and stop times
            DateTime sDate = DateTime.Parse(startDate);                         //.Dump("sDate")
            DateTime sDateUtc = TimeZoneInfo.ConvertTimeToUtc(sDate);           //.Dump("sDateUtc")
            var startTicks = sDateUtc.Ticks;
            DateTime eDate = DateTime.Parse(endDate);                           //.Dump("eDate")
            DateTime eDateUtc = TimeZoneInfo.ConvertTimeToUtc(eDate);           //.Dump("eDateUtc")
             

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
                        exec.Id = (long)reader[0];
                        exec.Account = (long)reader[1];
                        exec.BarIndex = (long)reader[2];
                        exec.Commission = (System.Double)reader[3];
                        exec.Exchange = (long)reader[4];
                        exec.ExecutionId = (string)reader[5];
                        exec.Fee = (double)reader[6];
                        exec.Instrument = (long)reader[7];
                        exec.IsEntry = (long)reader[8];
                        exec.IsEntryStrategy = (long)reader[9];
                        exec.IsExit = (long)reader[10];
                        exec.IsExitStrategy = (long)reader[11];
                        exec.LotSize = (long)reader[12];
                        exec.MarketPosition = (long)reader[13];
                        exec.MaxPrice = (System.Double)reader[14];
                        exec.MinPrice = (System.Double)reader[15];
                        exec.Name = (string)reader[16];
                        exec.OrderId = (string)reader[17];
                        exec.Position = (long)reader[18];
                        exec.PositionStrategy = (long)reader[19];
                        exec.Price = (System.Double)reader[20];
                        exec.Quantity = (long)reader[21];
                        exec.Rate = (double)reader[22];
                        exec.StatementDate = (long)reader[23];
                        exec.Time = (long)reader[24];
                        exec.ServerName = (string)reader[25];

                        //  add row to list
                        listExecution.Add(exec);
                    }
                    db.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                //  change IsEntry and IsExit to bool?
                foreach (var entry in listExecution)
                {
                    //  IsEntry to bool?
                    if (entry.IsEntry == 0)
                    {
                        entry.IsEntryB = false;
                    }
                    else
                    {
                        entry.IsEntryB = true;
                    }

                    //  IsExit to bool?
                    if (entry.IsExit == 0)
                    {
                        entry.IsExitB = false;
                    }
                    else
                    {
                        entry.IsExitB = true;
                    }
                }

            }


            ///<summary>
            ///<param>Select needed properties for Ret (instList return)</param>
            /// </summary>
            try
            {
                foreach (var execList in listExecution)
                {
                    //	create ListExecutionQueryClass
                    Ret.Ret list = new Ret.Ret();
                    {
                        //	fill new list 
                        list.InstId = (long?)0;
                        list.ExecId = execList.Id;
                        list.Name = symbol;
                        list.Account = execList.Account;
                        list.Position = execList.Position;
                        list.Quantity = execList.Quantity;
                        list.IsEntry = execList.IsEntryB;
                        list.IsExit = execList.IsExitB;
                        list.Price = execList.Price;
                        list.Time = execList.Time;
                        list.HumanTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime((long)execList.Time), TimeZoneInfo.Local).ToString("  HH:mm:ss MM/dd/yyyy  ");
                        list.Instrument = execList.Instrument;
                        list.P_L = 0;
                        list.Long_Short = "";

                        //	add to list
                        listExecutionRet.Add(list);
                    }
                }
                //  list from Executions table in Ret() format
                //  query this list with 'Instrument'
                //listExecutionRet.ToList();

            }
            catch
            {
                Console.WriteLine("foreach (var execList in listExecution)");
            }

            ///<summary>
            ///<param>query list from Executions in Ret() - listExecutionRets</param>
            ///<param>did not fill in expiry - found in Instruments with instrument #</param>
            /// </summary>
            try
            {
                instList = (from list in listExecutionRet
                                //where (Int64)list.Instrument == (Int64)62124056207858786      //  62124056207858786
                            where list.Time > (sDateUtc.Ticks)
                            select new Ret.Ret()
                            {
                                InstId = (long?)0,
                                ExecId = list.ExecId,
                                Account = list.Account,
                                Name = symbol,
                                Position = list.Position,
                                Quantity = list.Quantity,
                                IsEntry = list.IsEntry,
                                IsExit = list.IsExit,
                                Price = list.Price,
                                Time = list.Time,
                                HumanTime = list.HumanTime,
                                Instrument = list.Instrument,
                                Expiry = "Dec 2022",
                                P_L = 0,
                                Long_Short = ""

                            }).ToList();
                instList = instList.OrderByDescending(e => e.ExecId).ToList();

                // add Id to selectetRetList
                var instId = 0;
                foreach (Ret.Ret r in instList)
                {
                    r.InstId = instId;
                    instId++;
                }


            }
            catch
            {
                Console.WriteLine("query list from Executions");
            }

            return (List<Ret.Ret>)instList.ToList();

        }
    }
}
