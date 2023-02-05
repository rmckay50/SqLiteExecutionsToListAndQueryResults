using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using ExecutionsClass;
using Executions = ExecutionsClass.Executions;
using Query = ListExecutionQueryClass.ListExecutionQuery;
using GetInstListSqLite;
using Ret;




namespace SqLiteExecutionsToListAndQueryResults
{
    internal class Program
    {

            #region Set Parameters
        ////	Set to true for playback (account - 1)
        //  These parameters are passed from calling progran
        public static bool bPlayback = false;
        public static string name = "nq";
        public static string startDate = "  10:40:56 02/01/2023  ";
        // Set to "12/12/2022" to get all of data
        public static string endDate = "02/28/2022";
        #endregion Set Parameters
        static void Main(string[] args)
        {
            //var path = @"Data Source = C:\data\NinjaTrader.sqlite";
            var path = @"Data Source = C:\Users\Owner\Documents\NinjaTrader 8\db\NinjaTrader.sqlite";
            //  list to hold valiables in Executions table from NinjaTrader.sqlite
            List<Executions> listExecution = new List<Executions>();
            //  list to hold Ret() format from listExecution
            List<Ret.Ret> listExecutionRet = new List<Ret.Ret>();
            List<Query> selectedList = new List<Query>();
            //List<Ret.Ret> instList = new List<Ret.Ret>();
           //var instList = new List<Ret.Ret>();

            List<Query> listFromQuery = new List<Query>();

            //instList = (List<Ret.Ret>)Methods.getInstList(name, startDate, endDate, bPlayback);
            var instList = Methods.getInstList(name, startDate, endDate, bPlayback);


            //  Below is code from getInstList for filling in Expiry
            //  Expiry is located in Instruments 
            //  Can either load Instruments and do query or get info from chart 
            //  Will try char and in interim just add string 'Dec 2022'
            //  Format from .sdf instList was 'Dec 2022' 
            //  Expiry = new DateTime((long)mInsIns.Expiry).ToString(" MMM yyyy"),
            //  public string Expiry { get; set; }

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
                        entry.IsExitB= true;
                    }
                }

            }
                   #region Load 'Query' Commented out
                    /*
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
                    */
                    #endregion Load 'Query' Commented out


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
                    list.InstId =        (long?)0;
                    list.ExecId         = execList.Id;
                    list.Name           = symbol;
                    list.Account        = execList.Account;
                    list.Position       = execList.Position;
                    list.Quantity       = execList.Quantity;
                    list.IsEntry        = execList.IsEntryB;
                    list.IsExit         = execList.IsExitB;
                    list.Price          = execList.Price;
                    list.Time           = execList.Time;
                    list.HumanTime      = TimeZoneInfo.ConvertTimeFromUtc(new DateTime((long)execList.Time), TimeZoneInfo.Local).ToString("  HH:mm:ss MM/dd/yyyy  ");
                    list.Instrument     = execList.Instrument;
                    list.P_L            = 0;
                    list.Long_Short     = "";

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
                var instLi = (from list in listExecutionRet
                            //where (Int64)list.Instrument == (Int64)62124056207858786      //  62124056207858786
                            select new Ret.Ret()
                            {
                                InstId         = (long?)0,
                                ExecId         = list.ExecId,
                                Account        = list.Account,
                                Name           = symbol,
                                Position       = list.Position,
                                Quantity       = list.Quantity,
                                IsEntry        = list.IsEntry,
                                IsExit         = list.IsExit,
                                Price          = list.Price,
                                Time           = list.Time,
                                HumanTime      = list.HumanTime,
                                Instrument     = list.Instrument,
                                Expiry         = "Dec 2022",
                                P_L            = 0,
                                Long_Short     = ""

                            }).ToList();
                instList = instList.OrderByDescending(e => e.ExecId).ToList();

                // add Id to selectetRetList
                var instId = 0;
                foreach (var r in instList)
            {
                r.InstId = instId;
                instId++;
            }


            }
            catch
            {
                Console.WriteLine("query list from Executions");
            }
        }
    }
}


//Id Account	BarIndex	Commission	Exchange	ExecutionId	Fee	Instrument	IsEntry	IsEntryStrategy	IsExit	IsExitStrategy	LotSize	MarketPosition	MaxPrice	MinPrice	Name	OrderId	Position	PositionStrategy	Price	Quantity	Rate	StatementDate	Time	ServerName
//16633	2	-1	0	9	b6519f9200c84acb9d29002b46be94f7	0	62124056207858786	0	0	1	0	1	1	-1.79769313486232E+308	1.79769313486232E+308	Close	80929054cdad4a39a524760693980c2f	0	0	11873	1	1	638102016000000000	638102771851317160	ZBOOK

#region Check ability to create query - not needed
/*
try
{
    //	use query to create list
    //  original attempt to create list with format near Ret format
    var query = (from l in selectedList
                    where (Int64)l.Instrument == (Int64)62124056207858786      //  62124056207858786
                    select new Query()
                    {
                        Id         = l.Id,
                        Symbol     = symbol,
                        Instrument = l.Instrument,
                        IsEntry    = l.IsEntry,
                        IsExit     = l.IsExit,
                        Position   = l.Position,
                        Quantity   = l.Quantity,
                        Price      = l.Price,
                        Time       = l.Time,
                        HumanTime  = TimeZoneInfo.ConvertTimeFromUtc(new DateTime((long)l.Time), TimeZoneInfo.Local).ToString("  HH:mm:ss MM/dd/yyyy  "),

                        //HumanTime = new DateTime((long)l.Time)
                    }).ToList();
        query.ToList();
        listFromQuery = query.ToList();
}

catch
{
    Console.WriteLine("Query");
}
*/
#endregion Check ability to create query
