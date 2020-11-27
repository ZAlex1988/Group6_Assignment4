using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Models
{
    public class ChartResponse
    {
        public List<string> labels { get; set; }
        public List<string> colors { get; set; }

        public List<int> data { get; set; }

    }
}
