using Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionGetActiveEntry
{
    public static class Extension
    {
        #region GetActiveEntry - Finds applicable entry in Trades 
        //	On first pass ActiveEntry numbers have been set in Main()
        //	Get starting entry price row and values
        //	Start at first entry above exit and search for row that has entry = true and matched = false
        //	Record starting Id
        public static Source.Source GetActiveEntry(this Source.Source source, [CallerMemberName] string memberName = "", [CallerLineNumber] int LineNumber = 0)                                           //	GetActiveEntry
        {
            //Console.WriteLine("\nGetActiveEntry() Called by " + memberName + " () at line " + LineNumber + " / " + LN());
            //if (source.ActiveEntryRemaining == 0)
            //{
            var s = source.Trades[0].Id;
            //	start is first row above exit row
            int start = source.Trades[source.rowInTrades - 1].Id;
            for (int i = source.Trades[source.rowInTrades - 1].Id; i >= 0; i--)
            {

                int filler = 0;
                if (source.Trades[i].IsEntry == true && source.Trades[i].Matched == false)
                {
                    source.ActiveEntryId = source.Trades[i].Id;                                         //	GetActiveEntry
                    source.ActiveEntryPrice = source.Trades[i].Price;                                   //	GetActiveEntry	
                    source.ActiveEntryRemaining = source.Trades[i].Qty;                                   //	GetActiveEntry	
                                                                                                          //	2022 09 18  Problems here - on first pass from Main() source.ActiveEntryRemaining has been set to t.Qty
                                                                                                          //	Line above has been added but will probably not work on subsequent passes!
                                                                                                          //if (source.ActiveEntryRemaining == 0)
                                                                                                          //{
                                                                                                          //	source.ActiveEntryRemaining = source.Trades[i].Qty;                                 //	GetActiveEntry
                                                                                                          //}

                    break;
                    //return source;
                }
                //}
            }

            //Console.WriteLine("\nGetActiveEntry() Returned to " + memberName + "() at line " + LineNumber + " / " + LN());

            return source;
        }
        #endregion GetActiveEntry - Finds applicable entry in Trades 
        public static int LN([CallerLineNumber] int LN = 0)
        {
            return (LN);
        }

    }
}
