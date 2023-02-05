using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace NinjaTrader.Custom.AddOns
//namespace SqLiteExecutionsToListAndQueryResults
    namespace Ret

{
    //  needed to change some times to work with .sqlite format
    public class Ret
    {
        //  InstId is an Id for this class and is filled in later
        //public int InstId { get; set; }         // Created when instList is made
        public long? InstId { get; set; }         // Created when instList is made

        public long ExecId { get; set; }
        public long Account { get; set; }

        public string Name { get; set; }
        public long? Position { get; set; }
        public long? Quantity { get; set; }
        //public long? IsEntryL { get; set; }
        public bool? IsEntry { get; set; }

        public bool? IsExit { get; set; }
        //public bool? IsExitB { get; set; }

        public double? Price { get; set; }
        public long? Time { get; set; }
        public string HumanTime { get; set; }
        public long Instrument { get; set; }
        //Expiry is located innInstruent list and will no be used
       public string Expiry  { get; set; }
        public double? P_L { get; set; }
        public string Long_Short { get; set; }

        public Ret() { }

        public Ret(long? instId, long execId, long account, string name, long? position, long? quantity, bool? isEntryL, bool? isExit, double? price, long? time, string humanTime, long instrument, string expiry, double? p_L, string long_Short)
        {
            InstId = instId;
            ExecId = execId;
            Name = name;
            Account = account;
            Position = position;
            Quantity = quantity;
            IsEntry = isEntryL;
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
