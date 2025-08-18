using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shedding_Shield
{
   public class Schedule
    {
        private readonly Dictionary<string, (TimeSpan Start, TimeSpan Duration)> regions;

        public Schedule()
        {
            regions = new Dictionary<string, (TimeSpan, TimeSpan)>
            {
                { "Pretoria East", (new TimeSpan(18, 0, 0), new TimeSpan(4, 0, 0)) },
                { "Pretoria West", (new TimeSpan(16, 0, 0), new TimeSpan(4, 0, 0)) },
                { "Pretoria North", (new TimeSpan(20, 0, 0), new TimeSpan(4, 0, 0)) },
                { "Pretoria South", (new TimeSpan(14, 0, 0), new TimeSpan(4, 0, 0)) }
            };
        }

        public (TimeSpan Start, TimeSpan Duration) GetSchedule(string region)
        {
            if (!regions.ContainsKey(region))
                throw new ArgumentException("Invalid region selected.");
            return regions[region];
        }

        public IEnumerable<string> GetRegions()
        {
            return regions.Keys;
        }
    }
}
