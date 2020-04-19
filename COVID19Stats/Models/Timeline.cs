using System.Collections.Generic;

namespace COVID19Stats.Models
{
    public class Timeline
    {
        public Dictionary<string, int> Cases { get; set; }

        public Dictionary<string, int> Deaths { get; set; }

        public Dictionary<string, int> Recovered { get; set; }
    }
}
