using CSV;
using Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMatchAndAddToCsv
{
    public static class Extension
    {
        #region MatchAndAddToCsv
        //	Called from Fill
        //	Sets Matched to true for Exit and Entry in source.Trades
        //	Adds row to csv
        //	csv will not be in correct order to sort before exit
        public static Source.Source MatchAndAddToCsv(this Source.Source source, [CallerMemberName] string memberName = "", [CallerLineNumber] int LineNumber = 0)                                           //	MatchndAddToCsv
        {
            //Console.WriteLine("\nMatchAndAddToCsv() Called by " + memberName + " () at line " + LineNumber + " / " + LN());


            //	Do not set Matched on exit row until remaining == 0
            //	2022 09 03 0800 
            //	When ActiveEntryId changes that entry has been filled
            // 	 
            //if (source.PriorActiveEntryId != source.ActiveEntryId)											//	MatchndAddToCsv
            //{
            //	source.Trades[source.PriorActiveEntryId].Matched = true;									//	MatchndAddToCsv
            //}

            if (source.ActiveEntryRemaining == 0)                                                                       //	MatchndAddToCsv
            {
                source.Trades[source.ActiveEntryId].Matched = true;                                         //	MatchndAddToCsv
            }

            //// Set last row .Matched to true on last pass
            //if ( source.Trades.Count() - 1 == source.rowInTrades )											//	MatchndAddToCsv
            //{
            //	source.Trades[source.rowInTrades].Matched = true;											//	MatchndAddToCsv
            //}

            // Set last row .Matched to true on last pass
            if (source.Remaining == 0)                                            //	MatchndAddToCsv
            {
                //foreach (var t in source.Trades) 
                //{
                //	t.Matched = true;
                //}
                source.Trades[source.rowInTrades].Matched = true;                                           //	MatchndAddToCsv
            }

            // 	When Position == 0 all trades are matched
            //	When Position == 0 and source.Remaining == 0 set all .Matched to true
            if (source.Trades[source.rowInTrades].Position == 0 && source.Remaining == 0)                                            //	MatchndAddToCsv
                                                                                                                                     //	MatchndAddToCsv
            {
                for (int i = source.rowInTrades; i >= 0; i--)
                {
                    source.Trades[i].Matched = true;
                }

            }



            //ln.Dump("In MatchndAddToCsv()   " + LineNumber);                                                //	MatchndAddToCsv
            //Console.WriteLine($"\nsource.ExitQty = {source.ExitQty}");                                    //	MatchndAddToCsv

            //	Add line to csv
            CSV.CSV csv = new CSV.CSV()                                                                             //	MatchndAddToCsv
            {
                EntryId = source.ActiveEntryId,                                                             //	MatchndAddToCsv
                FilledBy = source.rowInTrades,                                                              //	MatchndAddToCsv
                Entry = source.ActiveEntryPrice,                                                            //	MatchAndAddToCsv
                Qty = source.ExitQty,                                                                       //	MatchndAddToCsv
                RemainingExits = source.Remaining,                                                          //	MatchndAddToCsv
                Exit = source.StartingExitPrice,               

            };
            Debug.WriteLine("EntryId = {csv.EntryId}");
            //Debug.Assert(csv.EntryId <= 75, csv.EntryId.ToString());

            //  debug - stop when csc.EntryId = 106
            Console.WriteLine(csv.EntryId);
            //Debug.Assert(csv.EntryId == 85,"Greaer than 85");
            if(csv.EntryId == 76 )
            {
                Console.WriteLine("sv.EntryId == 106");

			RemainingExits = source.Remaining,                                                          //	MatchndAddToCsv
			Exit = source.StartingExitPrice,                                                            //	MatchndAddToCsv
																										//Exit = source.Trades[source.RowInTrades].Price,
		};
		//	Add new line to CSV list
		source.Csv.Add(csv);                                                                            //	MatchndAddToCsv
                                                                                                        //	Update RowInCsv
            /// 2022 11 25 1325 Commented out to check for need																										//	Record row in CSV
            //source.RowInCsv++;                                                                              //	MatchndAddToCsv
            //	source.ExitQty is updated remainng quantity to match is quantity matched 
            ///	Try commenting this out
            //source.ExitQty = source.ExitQty - source.Remaining;

            //Console.WriteLine("\nMatchndAddToCsv() Returned to " + memberName + "() at line " + LineNumber + " / " + LN());

            return source;                                                                                  //	MatchndAddToCsv
        }
        #endregion MatchAndAddToCsv
        public static int LN([CallerLineNumber] int LN = 0)
        {
            return (LN);
        }

    }
}
