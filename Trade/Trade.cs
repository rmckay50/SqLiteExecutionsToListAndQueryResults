using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade
{
    public class Trade
    {
        public int Id { get; set; }
        public long ExecId { get; set; }                                                                     // 	class Trade
        public string Name { get; set; }
        public long? Position { get; set; }                                                                    // 	class Trade
        public int? Qty { get; set; }                                                                    // 	class Trade
        public bool? IsEntry { get; set; }                                                                 // 	class Trade
        public bool? IsExit { get; set; }                                                                  // 	class Trade
        public bool IsRev { get; set; }                                                                 // 	class Trade
        public bool Matched { get; set; }                                                               // 	class Trade
        public double? Price { get; set; }                                                               // 	class Trade
        public long? Time { get; set; }
        public string HumanTime { get; set; }
        public long Instrument { get; set; }
        public string Expiry { get; set; }
        public double? P_L { get; set; }
        public string Long_Short { get; set; }
        public int TradeNo { get; set; }                                                                // 	class Trade

        public IEnumerator GetEnumerator()                                                              // 	class Trade
        {
            return (IEnumerator)this;                                                                   // 	class Trade
        }

        public Trade() { }

        // Without int execId (second cstr, code runs
        //public Ret(int instId, int execId, string name, int? position, int? quantity, bool? isEntry, bool? isExit, double? price, long? time,
        //	string humanTime, long instrument, string expiry, double? p_L, string long_Short)
        public Trade(long execId, long? position, string name, int? qty, bool? isEntry, bool? isExit, double? price, long? time,
            string humanTime, long instrument, string expiry, double? p_L, string long_Short)
        {
            //InstId = instId;
            ExecId = execId;
            Position = position;
            Name = name;
            Qty = qty;
            IsEntry = isEntry;
            IsExit = isExit;
            Price = price;
            Time = time;
            HumanTime = humanTime;
            Instrument = instrument;
            Expiry = expiry;
            P_L = p_L;
            Long_Short = long_Short;
        }

        public Trade(int id)
        {
            Id = id;
        }
    }
}

