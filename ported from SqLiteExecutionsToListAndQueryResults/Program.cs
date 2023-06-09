﻿using System;
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
using Trade;
//using ExtensionCreateNTDrawline;
using ExtensionFill;
using ExtensionFillLongShortColumnInTradesList;
using ExtensionFillProfitLossColumnInTradesList;
using ExtensionCreateNTDrawline;
using LINQtoCSV;
using Parameters;
using System.IO;


//using getInstList;




namespace SqLiteExecutionsToListAndQueryResults
{
    public class Program
    { 
        #region Set Parameters
        ////	Set to true for playback (account - 1)
        //  These parameters are passed from calling progran
        public static bool bPlayback = false;
        public static string name = "nq";
        public static string startDate = "  10:40:56 03/22/2023  ";
        // Set to "12/12/2022" to get all of data
        public static string endDate = "03/24/2022";
        #endregion Set Parameters

        /// <summary>
        /// <switch between static void Main(string[] args) & public static void main(Paramaters.Input input)
        ///     to use for .exe or .dll
        ///     Parameters.Input input is filled in in callin program 
        /// <param name="input"></param>
        /// </summary>
        /// 
        //static void Main(string[] args)
        //  can use 'Input' becaue using statement is 'using Parameters.Paramaters;'
        //public static void main(Parameters.Input input, string output)
        public static void main(Parameters.Input input)


        {
            var timeIn = input.TimeLastBarOnChart;
            //  use first 2 strings for testing in VS
            //var timeFirstBarOnChart = DateTime.Parse("06/14/2008 7:30:00 PM");
            //var timeLastBarOnChart = DateTime.Parse("06/15/2008 2:12:13 PM");

            //  use these strings for .dll in NT need to add them to indicator
            //  will not build in VS
            //DateTime timeLastBarOnChart;
            //DateTime timeFirstBarOnChart;
            //timeFirstBarOnChart = ChartBars.GetTimeByBarIdx(ChartControl, ChartBars.FromIndex);
            //Print(string.Format("timeFirstBarOnChart is at {0}", timeFirstBarOnChart.ToString("yy MM dd HH_mm"))); //8/11/2015 4:30:00 AM  
            //timeLastBarOnChart = ChartBars.GetTimeByBarIdx(ChartControl, ChartBars.ToIndex);
            //Print(ChartBars.GetTimeByBarIdx(ChartControl, ChartBars.ToIndex)); //8/11/2015 4:30:00 AM  
            //Print(string.Format("lastBarOnChart is at {0}", timeLastBarOnChart.ToString("yy MM dd HH_mm"))); //8/11/2015 4:30:00 AM  



            //var path = @"Data Source = C:\data\NinjaTrader.sqlite";
            //var path = @"Data Source = C:\Users\Owner\Documents\NinjaTrader 8\db\NinjaTrader.sqlite";
            //var path = @"Data Source = \\RYZEN-2\NinjaTrader 8\db\NinjaTrader.sqlite";
            var path = input.InputPath;

            List<CSV.CSV> CSv = new List<CSV.CSV>();
            //  list to hold valiables in Executions table from NinjaTrader.sqlite
            List<Executions> listExecution = new List<Executions>();
            List<NTDrawLine.NTDrawLine> nTDrawline = new List<NTDrawLine.NTDrawLine>();
            //  list to hold Ret() format from listExecution
            List<Ret.Ret> listExecutionRet = new List<Ret.Ret>();
            Source.Source source = new Source.Source();
            List<Query> selectedList = new List<Query>();
            List<Trade.Trade> workingTrades = new List<Trade.Trade>();
            List<Trade.Trade> trades = new List<Trade.Trade>();

            ///
            ///<summary>
            /// <as.exe> use the line 'Input input = new Input()'</as.exe>
            /// <as.dll> use the section  ''</as.dll>
            /// 
            /// </summary>
            /// 
            #region Uncomment for use as .exe
            //Input exeInput = new Input()
            //{
            //    BPlayback = false,
            //    Name = "nq",
            //    StartDate = "01/02/2022",
            //    EndDate = "02/05/2023"
            //};
            #endregion Uncomment for use as .exe

            var instList = Methods.getInstList
                (
            #region Uncomment for use as .exe
                //name,
                //startDate,
                //endDate,
                //bPlayback,
                //@"
            #endregion Uncomment for use as .exe

            #region Uncomment for use as .dll
                input.Name,
                input.StartDate,
                input.EndDate,
                input.BPlayback,
                input.InputPath
            #endregion Uncomment for use as .dll
                );

            #region Create workingTrades

            //  NT runs through this section more than once
            //      Allow only one pass
            if (trades.Count == 0)
            {
                //	Create 'workingTrades' list																		//	Main
                //	Slimmed down instList that is added to source list to make transfer to extension easier
                // 	foreach through instList and add to trades list

                foreach (var inst in instList)
                {
                    trades.Add(new Trade.Trade(inst.ExecId, inst.Position, inst.Name, inst.Quantity, inst.IsEntry, inst.IsExit, inst.Price,
                        inst.Time, inst.HumanTime, inst.Instrument, inst.Expiry, inst.P_L, inst.Long_Short));
                }
            }
            //	Top row in Trades is last trade.  Position should be zero.  If not db error or trade was exited 
            //		next day
            //	Check that position is flat
            //if (t.Id == 0 && t.IsExit == true)
            try
            {
                if (trades[0].Position != 0)

                {
                    Console.WriteLine(@"Postion on - not flat");                                                  //	Main
                    Console.WriteLine(string.Format("Trades position = {0}", (trades[0].Position)));        //System.Environment.Exit(-1);                                                                //	Main																
                }
            }
            catch
            {
            }
            //	Top row is now first trade in selected list - Position != 0
            trades.Reverse();
            workingTrades = trades.ToList();
            trades.Clear();
            #endregion Create workingTrades				

            #region Code from 'Fill finList Prices Return List and Csv from Extension'							//	Main

             #region Create Lists
            //  Lists added to source which is used in extensions
            source.Trades = workingTrades;
            //  Added to keep source.csv from accumulating
            //source.Csv.Clear();
            source.Csv = CSv;                                                                                   //	Main
            source.NTDrawLine = nTDrawline;
            #endregion Create Lists													
           
             #region Initialize flags and variables in source
            //	'rowInTrades' is increased on each pass of foreach(var t in workingTrades)
            //	It is number of the row in trades that is either an Entry, Exit, or Reversal
            source.rowInTrades = 0;
            //	Commented out - not needed
            //source.RowInCsv = 0;                                                                                //	Main

            //	isReversal is flag for reversal
            source.IsReversal = false;                                                                          //	Main
            #endregion Initialize flags and variables in source

            #region Fill in Id	Not used on Ryzen-2																				//	Main
            //	Add Id to workingTrades
            int i = 0;                                                                                          //	Main

            foreach (var t in workingTrades)                                                                        //	Main
            {
                t.Id = i;                                                                                       //	Main
                i++;                                                                                            //	Main
            }
            #endregion Fill in Id																					//	Main

            #region foreach() through source.Trades
            foreach (var t in source.Trades)                                                                    //	Main
            {
                //Console.WriteLine("Line " + LN() + "  In Main()/ t.Id = " + t.Id);

                //	Record size of first entry and Id
                //	Need to keep record of how many entries are matched on split exits
                //	Updated in UpdateActiveEntery()
                if (t.Id == 0 && t.IsEntry == true)
                {
                    //  having a problem with execution Id not being accessed
                    source.ActiveEntryId = t.Id;                                                                //	Main
                    source.ActiveEntryRemaining = t.Qty;                                                        //	Main
                    source.ActiveEntryPrice = t.Price;                                                          //	Main
                }

                //	Is trade a normal exit?
                //	If previous trade was reversal the source.Trades.IsRev is == true
                //if (t.Entry == false && t.Exit == true && source.Trades[source.rowInTrades - 1].IsRev == false) //	Main
                if (t.IsEntry == false && t.IsExit == true) //	Main
                {
                    source.Fill();
                }


                //	Set reversal flags row numbers
                if (t.IsEntry == true && t.IsExit == true)                                                          //	Main
                {
                    //	Set source.IsReversal = true - used to break out of Fill()
                    source.IsReversal = true;                                                                   //	Main
                    source.RowOfReverse = source.rowInTrades;                                                   //	Main
                    source.RowInTrades = source.rowInTrades;                                                    //	Main
                    source.rowInTrades = source.rowInTrades;                                                    //	Main
                    source.Fill();                                                                              //	Main		
                }

                source.rowInTrades++;  // = rowInTrades;														//	Main
                                       //	Increase source.rowInTrades it was cycled through in the Fill extension
                source.RowInTrades++;                                                                           //	Main
            }

            #endregion foreach() through source.Trades

            #endregion Code from 'Fill finList Prices Return List and Csv from Extension'										

            #region FillLongShortColumnInTradesList														//	Main
            //	Call extenstion 'FillLongShortColumnInTradesList()' to fill in Long_Short column in workingTrades 
            source.FillLongShortColumnInTradesList();

            //souurce.FillLongShortColumnInTradesList();
            #endregion FillLongShortColumnInTradesList

            #region foreach through .csv and add StartTimeTicks StartTime ExitTimeTicks ExitTime Long_Short
            //  fill in csv.Entry.ID
            //  may be causing problems with setting start time and price
            //  commented out
            //int entryId = 0;
            //foreach (var entry in source.Csv)
            //{
            //    entry.EntryId = entryId;
            //    entryId++;
            //}

            foreach (var csv in source.Csv)
            {
                //	fill in blank spaces from workingTrades with time ans tickd//

                csv.Name = workingTrades[csv.EntryId].Name;
                csv.StartTimeTicks = workingTrades[csv.EntryId].Time;
                csv.StartTime = workingTrades[csv.EntryId].HumanTime;
                csv.EndTimeTicks = workingTrades[csv.FilledBy].Time;
                csv.EndTime = workingTrades[csv.FilledBy].HumanTime;
                csv.Long_Short = workingTrades[csv.EntryId].Long_Short;
            }

            #endregion foreach through .csv and add StarTimeTicks StartTime ExitTimeTicks ExitTime

            #region Fill in P_L coulmn in source.csv
            //	Call 'FillProfitLossColumnInTradesList' to fill in csv P_L column
            source.FillProfitLossColumnInTradesList();
            //source.
            #endregion Fill in P_L coulmn in source.csv

            #region Create NTDrawLine list for use in saving to file and later in NT

            source.NTDrawLine = source.CreateNTDrawline();

            #endregion Create NTDrawLine list for use in saving to file and later in NT

            #region Use LINQtoCSV on combined list to write
            //  foreach through source.NTDrawLine to create list with correct order for cc.write
            //  uses 'NTDrawLineForLINQtoCSV' which has column attributes
            var columnsWithAttributes = from l in source.NTDrawLine
                                        select new NTDrawLineForLINQtoCSV
                                        {
                                            Id = l.Id,
                                            Symbol = l.Symbol,
                                            Long_Short = l.Long_Short,
                                            StartTimeTicks = l.StartTimeTicks,
                                            StartTime = l.StartTime,
                                            StartY = l.StartY,
                                            EndTimeTicks = l.EndTimeTicks,
                                            EndTime = l.EndTime,
                                            EndY = l.EndY,
                                            P_L = l.P_L,
                                        };
            columnsWithAttributes.ToList();

            CsvFileDescription scvDescript = new CsvFileDescription();
            CsvContext cc = new CsvContext();
            cc.Write
            (
                columnsWithAttributes,
                input.OutputPath
            );

            //  replace name (local declaration) to input.Name (calling program definition)
            //  var fileName = name.ToUpper() + " " + DateTime.Now.ToString("yyyy MM dd   HH mm ss") + ".csv";
            //var fileName = exeInput.Name.ToUpper() + " " + DateTime.Now.ToString("yyyy MM dd   HH mm ss") + ".csv";
            var fileName = input.Name.ToUpper() + "                " + input.TimeLastBarOnChart + ".csv";
            var dir = Path.GetDirectoryName(input.OutputPath); ;

            if (input.BPlayback != true)
            {
                cc.Write(columnsWithAttributes, dir + @"\" + fileName);
            }
            else
            {
                //  lastBarTime is set in NT 'ChartBars.GetTimeByBarIdx(ChartControl, ChartBars.ToIndex)); //8/11/2015 4:30:00 AM'
                string l = input.TimeLastBarOnChart;
                fileName = input.Name.ToUpper() + " Playback " + input.TimeFirstBarOnChart + " To " + input.TimeLastBarOnChart + ".csv";
                cc.Write(columnsWithAttributes, dir + @"\" + fileName);
            }




            //cc.Write(source.NTDrawLine, output);

            #endregion


        }
    }
}


//Id Account	BarIndex	Commission	Exchange	ExecutionId	Fee	Instrument	IsEntry	IsEntryStrategy	IsExit	IsExitStrategy	LotSize	MarketPosition	MaxPrice	MinPrice	Name	OrderId	Position	PositionStrategy	Price	Quantity	Rate	StatementDate	Time	ServerName
//16633	2	-1	0	9	b6519f9200c84acb9d29002b46be94f7	0	62124056207858786	0	0	1	0	1	1	-1.79769313486232E+308	1.79769313486232E+308	Close	80929054cdad4a39a524760693980c2f	0	0	11873	1	1	638102016000000000	638102771851317160	ZBOOK

#region Changed to getInstList.dll
/*

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

            if ( input.BPlayback != true )
            {
                cc.Write(columnsWithAttributes, dir + @"\" + fileName);
            }
            else
            {
                fileName = input.Name.ToUpper() + " Playback " + input.TimeFirstBarOnChart + " To " + input.TimeLastBarOnChart + ".csv";
                cc.Write(columnsWithAttributes, dir + @"\" + fileName);
            }
            #endregion Use LINQtoCSV on combined list to write
        }
*/
#endregion Changed to getInstList.dll

        public class NTDrawLineForLINQtoCSV
        {
            [CsvColumn(FieldIndex = 1)]
            public int Id { get; set; }
            [CsvColumn(FieldIndex = 2)]
            public string Symbol { get; set; }
            [CsvColumn(FieldIndex = 3)]
            public string Long_Short { get; set; }
            [CsvColumn(FieldIndex = 4)]
            public long StartTimeTicks { get; set; }
            [CsvColumn(FieldIndex = 5)]
            public string StartTime { get; set; }
            [CsvColumn(FieldIndex = 6)]
            public double StartY { get; set; }
            [CsvColumn(FieldIndex = 7)]
            public long EndTimeTicks { get; set; }
            [CsvColumn(FieldIndex = 8)]
            public string EndTime { get; set; }
            [CsvColumn(FieldIndex = 9)]
            public double EndY { get; set; }
            [CsvColumn(FieldIndex = 10)]
            public double P_L { get; set; }
        }



