using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameters
{
    namespace Paramaters
    {
        public class Input
        {
            public bool BPlayback { get; set; }

            public string Name { get; set; }

            public string StartDate { get; set; }

            public string EndDate { get; set; }
            public string Path { get; set; }

            public Input()
            {
            }

            public Input(bool bPlayback, string name, string startDate, string endDate, string path)
            {
                BPlayback = bPlayback;
                Name = name;
                StartDate = startDate;
                EndDate = endDate;
                Path = path;
            }
        }
    }
}
