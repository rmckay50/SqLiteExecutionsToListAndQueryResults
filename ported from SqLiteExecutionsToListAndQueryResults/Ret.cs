using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace NinjaTrader.Custom.AddOns
namespace SqLiteExecutionsToListAndQueryResults
{ 
    //  needed to change some times to work with .sqlite format
    internal class Ret
    {
        //public int InstId { get; set; }         // Created when instList is made
        public long InstId { get; set; }         // Created when instList is made

        public int ExecId { get; set; }
        public string Name { get; set; }
        public int? Position { get; set; }
        public int? Quantity { get; set; }
        public bool? IsEntry { get; set; }
        public bool? IsExit { get; set; }
        public double? Price { get; set; }
        public long? Time { get; set; }
        public string HumanTime { get; set; }
        public long Instrument { get; set; }
        public string Expiry { get; set; }
        public double? P_L { get; set; }
        public string Long_Short { get; set; }

        public Ret() { }

        public Ret(int instId, int execId, string name, int? position, int? quantity, bool? isEntry, bool? isExit, double? price, long? time, string humanTime, long instrument, string expiry, double? p_L, string long_Short)
        {
            InstId = instId;
            ExecId = execId;
            Name = name;
            Position = position;
            Quantity = quantity;
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




    }
}
