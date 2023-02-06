using Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionFillProfitLossColumnInTradesList
{
    public static class Extension
    {
        #region Fill in workingTrades P/L column

        public static Source.Source FillProfitLossColumnInTradesList(this Source.Source source, [CallerMemberName] string memberName = "", [CallerLineNumber] int LineNumber = 0)
        {
            foreach (var pl in source.Csv)
            {
                // 	Check for null - condition when prices have not been filled in yet in finList
                if (pl.Exit.HasValue && pl.Entry.HasValue)
                {
                    // long
                    if (pl.Long_Short == "Long")
                    {
                        try
                        {
                            // Exception for ExitPrice is null -- caused by making calculation on partial fill before price columns are filled in
                            //ln = LineNumber();
                            //ln.Dump("long try blockExitPrice");


                            //fl.P_L = (double)fl.ExitPrice.Dump("long try blockExitPrice") - (double)fl.EntryPrice.Dump("EntryPrice");
                            pl.P_L = (double)pl.Exit - (double)pl.Entry;

                            pl.P_L = Math.Round((Double)pl.P_L, 2);
                        }
                        catch
                        {
                            //ln = LineNumber();
                            //ln.Dump("long catch block");
                            //pl.Exit.Dump("catch ExitPrice");
                            //pl.Entry.Dump("EntryPrice");
                        }
                    }

                    // short
                    if (pl.Long_Short == "Short")
                    {
                        try
                        {
                            pl.P_L = (double)pl.Entry - (double)pl.Exit;
                            pl.P_L = Math.Round((Double)pl.P_L, 2);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            return source;

        }

        #endregion Fill in workingTrades P/L column
        public static int LN([CallerLineNumber] int LN = 0)
        {
            return (LN);
        }

    }
}
