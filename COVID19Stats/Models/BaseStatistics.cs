using System;

namespace COVID19Stats.Models
{
    public class BaseStatistics
    {
        public long Updated { get; set; }

        public int Cases { get; set; }

        public int Deaths { get; set; }

        public int Recovered { get; set; }

        public int Active { get; set; }

        public int Critical { get; set; }

        public int Tests { get; set; }

        public float TestsPerOneMillion { get; set; }

        public DateTime UpdatedDate
        {
            get
            {
                // Unix timestamp is seconds past epoch
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddMilliseconds(Updated).ToLocalTime();
                return dtDateTime;
            }
        }
    }
}
