using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seagulls.Models
{
    public class StatsViewModel
    {
        public virtual List<BattingStatsViewModel> BattingStats { get; set; }
        public virtual List<BowlingStatsViewModel> BowlingStats { get; set; }
    }
}