using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameters
{
    public class Input
    {
        public bool BPlayback { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public string LastBarTime { get; set; }

        public Input()
        {
        }

        public Input(bool bPlayback, string name, string startDate, string endDate, string inputPath, string outputPath, string lastBarTime)
        {
            BPlayback = bPlayback;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            InputPath = inputPath;
            OutputPath = outputPath;
            LastBarTime = lastBarTime;
        }
    }
}
