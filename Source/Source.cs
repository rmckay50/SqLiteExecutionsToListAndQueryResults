using ExtensionCreateNTDrawline;
using NTDrawLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade;

namespace Source
{
    public class Source //: IEnumerable																	
    {
        //public int PriorActiveEntryId { get; set; }														//	class source
        public int ActiveEntryId { get; set; }                                                          //	class source
        public long? ActiveEntryRemaining { get; set; }                                                   //	class source
        public double? ActiveEntryPrice { get; set; }                                                    //	class source
        public bool IsReversal { get; set; }                                                            //	class source
        public long PositionAfterReverse { get; set; }                                                   //	class source
        public long RowOfReverse { get; set; }                                                           //	class source
        public long Position { get; set; }                                                               //	class source
        public double? StartingExitPrice { get; set; }                                                   //	class source
        public int rowInTrades { get; set; }                                                            //	class source
        public int RowInTrades { get; set; }                                                            //	class source
        //public int RowInCsv { get; set; }                                                               //	class source
        public long? ExitQty { get; set; }                                                                //	class source
        public long? Remaining { get; set; }                                                              //	class source
        public List<Trade.Trade> Trades { get; set; }                                                         //	class source
        public List<CSV.CSV> Csv { get; set; }                                                              //	class source
        public List<NTDrawLine.NTDrawLine> NTDrawLine { get; set; }

    }
}
