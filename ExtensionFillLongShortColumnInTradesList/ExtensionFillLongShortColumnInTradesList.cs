using Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionFillLongShortColumnInTradesList
{
    public static class Extension
    {
        #region FillLongShortColumnInTradesList
        // 	Determines whether entry is a long or short position and fills in Long_Short column for entries
        //	Exits are left as null
        //	Called from Main()
        // 	Extenstion to fill Exit info
        //	Called from 'Check for normal exit (Entry == false - Exit == true)'
        //	Finds entry and exit prices
        //	All source.ActiveEntry... vars are found in 'UpdateActiveEntry' - allows Fill() and PartialFill() to use same code

        public static Source.Source FillLongShortColumnInTradesList(this Source.Source source, [CallerMemberName] string memberName = "", [CallerLineNumber] int LineNumber = 0)
        {
            //	Fill() Called by Main () at line 449 / 522
            Console.WriteLine("\nFillLongShortColumnInTradesList() Called by " + memberName + " () at line " + LineNumber + " / " + LN());
            //	Order is set to top entry being the last trade
            //	Position value will be zero
            //	foreach through list and compare previous position to current position
            //	Increase in position after entry == long while decrease after entry == short		
            //source.Trades.Reverse();
            //	Need to renumber Id column
            int tradesId = 0;
            long? lastPosition = 0;

            foreach (var id in source.Trades)
            {
                id.Id = tradesId;
                tradesId++;
            }
            //	longShort = ""	is set to null on entry
            //	will only be null on start of foreach
            string longShort = "";
            foreach (var ls in source.Trades)
            {
                ////	Exit if first trade is not an exit
                //if (ls.Id == 0 && ls.IsExit == false)
                //{
                //	Console.WriteLine(@"First trade is exit");                                                  //	FillLongShortColumnInTradesList
                //																								//	FillLongShortColumnInTradesList
                //	System.Environment.Exit(-1);                                                                //	FillLongShortColumnInTradesList																
                //}
                #region First trade fill in 'Long' or 'short'
                // Fill in Long_Short column
                // Is entry buy or sell first trade (LongShort is initialized to "")
                if (longShort == "")
                {
                    if (ls.Position <= -1)
                    {
                        //position = Position.Short; 
                        longShort = "Short";
                        //lastPosition = -1;
                        //break;
                    }
                    else if (ls.Position >= 1)
                    {
                        //position = Position.Long;
                        longShort = "Long";
                        //lastPosition = 1;
                        //break;
                    }
                }
                #endregion

                #region Fill in 'Long' or 'Short' column for remaining entries in trade
                // If position size increases (positive) trade was a long
                //	Fill in posList 'Long_Short' with "Long"
                if (ls.IsEntry == true && ls.Position > lastPosition)
                {
                    //position = Position.Long;
                    longShort = "Long";
                    lastPosition = ls.Position;
                    ls.Long_Short = longShort;

                    //lastPosition.Dump();
                }

                // If position size increases (negative) trade was a short
                //	Fill in posList 'Long_Short' with "Short"

                else if (ls.IsEntry == true && ls.Position < lastPosition)
                {
                    //position = Position.Short;
                    longShort = "Short";
                    lastPosition = ls.Position;
                    ls.Long_Short = longShort;
                    //lastPosition.Dump();
                }

                //	Fill in posList Long_Short column with "null" if trade is an exit
                if (ls.IsExit == true)

                //if (ao.IsExit == true
                //	|| ao.IsExit == true && ao.IsEntry == true)

                {
                    longShort = null;
                    lastPosition = ls.Position;
                    ls.Long_Short = null;

                    //lastPosition.Dump();

                }
                #endregion

            }



            Console.WriteLine("\nFill() Returned to " + memberName + "() at line " + LineNumber + " / " + LN());

            return source;                                                                                  //	Fill
        }
        #endregion FillLongShortColumnInTradesList
        public static int LN([CallerLineNumber] int LN = 0)
        {
            return (LN);
        }

    }
}
