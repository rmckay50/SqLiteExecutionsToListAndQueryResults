using NTDrawLine;
using Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionCreateNTDrawline
{
    public static class Extension
    {
         
        #region Create NTDrawline for save to .csv

        public static List<NTDrawLine.NTDrawLine> CreateNTDrawline(this Source.Source source, [CallerMemberName] string memberName = "", [CallerLineNumber] int LineNumber = 0)
        {
            // NTDrawLine -> Id, EntryPrice, EntryTime(ticks), ExitPrice, ExitTime(ticks)
            List<NTDrawLine.NTDrawLine> nTDrawLine = new List<NTDrawLine.NTDrawLine>();
            // Counter for nTDrawLine lines

            foreach (var csv in source.Csv)
            {
                try
                {
                    var t = DateTime.Parse(csv.StartTime).Ticks;

                    nTDrawLine.Add(
                        new NTDrawLine.NTDrawLine
                        (
                            0,
                            csv.Name,
                            csv.Long_Short,
                            (long)csv.StartTimeTicks,
                            DateTime.Parse(csv.StartTime).ToString("HH:mm:ss  MM/dd/yyy"),
                             (double)csv.Entry,
                            (long)csv.EndTimeTicks,
                            DateTime.Parse(csv.EndTime).ToString("HH:mm:ss  MM/dd/yyy"),
                            (double)csv.Exit,
                            (double)csv.P_L

                        )
                    );


                }
                catch
                {
                    //csv.Exit.Dump();

                }
            }
            int nTDrawLineId = 0;
            foreach (var e in nTDrawLine)
            {
                e.Id = nTDrawLineId;
                nTDrawLineId++;
            }

            return nTDrawLine;
        }

        #endregion Create NTDrawline for save to .csv
    }
}
