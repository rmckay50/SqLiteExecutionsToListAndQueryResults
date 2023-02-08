using System;
using System.Collections.Generic;
using ExtensionGetActiveEntry;
using ExtensionMatchAndAddToCsv;
using ExtensionUpdateActiveEntry;
using System.Linq;
using Source;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionFill
{
    public static class Extension
    {
        #region Fill
        // 	Extenstion to fill Exit info
        //	Called from 'Check for normal exit (Entry == false - Exit == true)'
        //	Finds entry and exit prices
        //	All source.ActiveEntry... vars are found in 'UpdateActiveEntry' - allows Fill() and PartialFill() to use same code

        public static Source.Source Fill(this Source.Source source, [CallerMemberName] string memberName = "", [CallerLineNumber] int LineNumber = 0)
        {
            //	Fill() Called by Main () at line 449 / 522
            //Console.WriteLine("\nFill() Called by " + memberName + " () at line " + LineNumber + " / " + LN());

            //	Get Qty of Exit 
            //	Set source.ExitQty and source.Remaining to quantity in Exit row
            //	These numbers should be updated on each match with an Entry for following progress
            //	Example 
            //	ExitQty		Remaining
            //		3			3
            //		2			1
            //		1			0		prece
            //	On partial fill this doesn't work 
            //	source.remaining should be number of unmatched entries from preceeding entry - not necessarily the most
            //		recent entry.  Adjust only if source.ActiveEntryRemaining = 0

            //	Get starting entry info
            //	Looks for first entry above (matched == false) 
            //  If partial fill need to go higher to check all entries

            //	2022 09 18 1000
            //	RowInTrades not set on 2nd exit 
            //	Try setting on entry 
            source.RowInTrades = source.rowInTrades;
            source.GetActiveEntry();

            //	Number of exits that have not been matched
            source.Remaining = source.Trades[source.RowInTrades].Qty;                                       //	Fill			

            //	Save exit price - it is used for all entry matches
            source.StartingExitPrice = source.Trades[source.RowInTrades].Price;                             //	Fill

            while (source.Remaining > 0)                                                                        //	Fill
            {

                source.UpdateActiveEntry();                                                             //	Fill
                source.MatchAndAddToCsv();                                                              //	Fill

                //	Break on source.IsReversal = true;
                //	Only need one pass

                if (source.IsReversal == true)
                {
                    source.IsReversal = false;
                    return source;
                }
                //	 Fill

                //	Back up through trades to match all fills
                //	rowInTades keeps track of the row in Trades as each line is processed
                //	while source.RowInTrades is used to back up through Trades list to find matched entries
                source.RowInTrades--;                                                                       //	Fill
            }
            //Console.WriteLine("\nFill() Returned to " + memberName + "() at line " + LineNumber + " / " + LN());

            return source;                                                                                  //	Fill
        }
        #endregion Fill
        public static int LN([CallerLineNumber] int LN = 0)
        {
            return (LN);
        }

    }
}
