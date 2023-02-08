using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Source;
using System.Text;
using System.Threading.Tasks;


namespace ExtensionUpdateActiveEntry
{
    public static class Extension
    {
        #region UpdateActiveEntry
        //	Subtracts qty of exits from first entry that is not filled
        //	To get next open entry after source.ActiveEntryRemaining == 0 work down from top to find first .Matched == false
        //	All exits will be matched
        public static Source.Source UpdateActiveEntry(this Source.Source source, [CallerMemberName] string memberName = "", [CallerLineNumber] int LineNumber = 0)                                            //	UpdateAtiveEntry
        {
            //Console.WriteLine("\nUpdateActiveEntry() Called by " + memberName + " () at line " + LineNumber + " / " + LN());
            //Console.WriteLine($"\nsource.Remaining = {source.Remaining}" + " at line " + LN());                                //	UpdateAtiveEntry
            //Console.WriteLine($"\nsource.ExitQty = {source.ExitQty}" + " at line " + LN());                                	//	UpdateAtiveEntry

            //	Subtract qty of exits from source.ActiveEntryRemaining
            //	When source.ActiiveEntryRemaining == 0 find next open entry
            //	Initialized in Main() on first foreach()
            //		source.ActiveEntryRemaining = t.Qty
            if (source.ActiveEntryRemaining == 0)                                                           //	UpdateAtiveEntry		
            {
                //source.PriorActiveEntryId = source.ActiveEntryId;											//	UpdateAtiveEntry
                //for (int i = 0; i < source.rowInTrades; i++)
                for (int i = source.rowInTrades - 1; i >= 0; i--)                                       //	UpdateAtiveEntry

                {
                    // 	Start at top of trades and work down to first unmatched row
                    //	Doesn't work for FIFO if multiple entries and then close - will get first not last
                    if (source.Trades[i].Matched == false)
                    {
                        source.ActiveEntryId = i;                                                           //	UpdateAtiveEntry
                        source.ActiveEntryRemaining = source.Trades[i].Qty;                                 //	UpdateAtiveEntry
                        source.ActiveEntryPrice = source.Trades[i].Price;                                   //	UpdateActiveEntry
                        break;                                                                              //	UpdateAtiveEntry
                    }
                }
            }
            //else
            //{
            //	source.PriorActiveEntryId = source.ActiveEntryId;											//	UpdateAtiveEntry
            //}

            //	Moved from Fill()
            #region Get ActiveEntry... calculation to 'UpdateActiveEntry'
            //	Moved to GetActiveEntry
            //	Get starting entry price row and values
            //	Start at first entry above exit and search for row that has entry = true and matched = false
            //	Record starting Id
            //int start = source.Trades[source.rowInTrades - 1].Id;
            //for (int i = source.Trades[source.rowInTrades - 1].Id; i >= 0; i--)                         //	UpdateActiveEntry
            //{
            //	
            //	int filler = 0;                                                                         //	UpdateActiveEntry								
            //	if (source.Trades[i].Entry == true && source.Trades[i].Matched == false)                //	UpdateActiveEntry
            //	{
            //		source.ActiveEntryId = source.Trades[i].Id;                                         //	UpdateActiveEntry
            //		source.ActiveEntryPrice = source.Trades[i].Price;                                 	//	UpdateActiveEntry	
            //		source.ActiveEntryRemaining = source.Trades[i].Qty;                                 //	UpdateActiveEntry
            //		break;
            //	}
            //}
            #endregion Get ActiveEntry... calculation to 'UpdateActiveEntry'

            #region Set source.ExitQty
            //	Set source.ExitQty and pass to MatchAndAddToCsv()
            //	This should be after next block because source.ActiveEntryRemaining will be changed
            if (source.Remaining >= source.ActiveEntryRemaining)

            {
                source.ExitQty = source.ActiveEntryRemaining;                                               //	UpdateActiveEntry
            }
            else
            {
                source.ExitQty = source.Remaining;                                                          //	UpdateActiveEntry
            }
            #endregion Set source.ExitQty

            source.ActiveEntryRemaining = source.ActiveEntryRemaining - source.ExitQty;                     //	UpdateAtiveEntry		


            //Console.WriteLine($"source.ActiveEntryId = {source.ActiveEntryId}");              				//	UpdateAtiveEntry
            //Console.WriteLine($"source.ActiveEntryRemaining = {source.ActiveEntryRemaining}");              //	UpdateAtiveEntry
            //Console.WriteLine($"source.rowInTrades = {source.rowInTrades}");                                //	UpdateAtiveEntry
            source.Remaining = source.Remaining - source.ExitQty;                                           // 	UpdateAtiveEntry

            //Console.WriteLine($"\nsource.Remaining = {source.Remaining}" + " at line " + LN());                                    //	UpdateAtiveEntry
            //Console.WriteLine($"\nsource.ExitQty = {source.ExitQty}" + " at line " + LN());                                        //	UpdateAtiveEntry

            //source.Csv.Dump("Csv");
            //Console.WriteLine("Csv");
            //Console.WriteLine("\nUpdateAtiveEntry() Returned to " + memberName + " () at line " + LineNumber + " / " + LN());

            return source;                                                                                  //	UpdateAtiveEntry

        }
        public static int LN([CallerLineNumber] int LN = 0)
        {
            return (LN);
        }


        #endregion UpdateActiveEntry
    }
}
