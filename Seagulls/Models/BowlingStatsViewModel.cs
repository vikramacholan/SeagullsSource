using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seagulls.Models
{
    public class BowlingStatsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Overs { get; set; }

        public decimal Balls { get; set; }

        public int Runs { get; set; }

        public decimal RunsPerOver { get; set; }

        public decimal StrikeRate { get; set; }

        public int Wickets { get; set; }

        public int ThreeWickets { get; set; }

        public int FiveWickets { get; set; }

        public int Wides { get; set; }

        public int NoBalls { get; set; }

        public int BestWickets { get; set; }
        public int BestRuns { get; set; }

    }
}