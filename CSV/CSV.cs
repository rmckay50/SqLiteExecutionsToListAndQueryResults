using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV
{
    public class CSV
    {
        //public int EntryId { get; set; }
        public int EntryId { get; set; }                                                                //	class CSV
        public int FilledBy { get; set; }                                                               //	class CSV
        public int? Qty { get; set; }                                                                    //	class CSV
        public int? RemainingExits { get; set; }                                                         //	class CSV
        public long? StartTimeTicks { get; set; }
        public string StartTime { get; set; }
        public double? Entry { get; set; }                                                               //	class CSV
        public long? EndTimeTicks { get; set; }
        public string EndTime { get; set; }
        public double? Exit { get; set; }                                                                //	class CSV
        public string Long_Short { get; set; }
        public double P_L { get; set; }


        public IEnumerator GetEnumerator()                                                              //	class CSV
        {
            return (IEnumerator)this;                                                                       //	class CSV
        }
    }
}
