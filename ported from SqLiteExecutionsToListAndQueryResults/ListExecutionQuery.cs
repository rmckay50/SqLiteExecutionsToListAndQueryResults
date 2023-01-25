using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListExecutionQueryClass
{
    internal class ListExecutionQuery
    {
        public Int64 Id { get; set; }
        public string Symbol { get; set; }
        public Int64 Instrument { get; set; }
        public Int64? IsEntry { get; set; }
        public Int64? IsExit { get; set; }
        public Int64? Position { get; set; }
        public Int64? Quantity { get; set; }
        public Double? Price { get; set; }
        public Int64? Time { get; set; }
        public string HumanTime { get; set; }

        public ListExecutionQuery() { }

    }
}
