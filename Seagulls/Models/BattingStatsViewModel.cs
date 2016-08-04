using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seagulls.Models
{
    public class BattingStatsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int InningsCount { get; set; }
        public int Runs { get; set; }

        public int Balls { get; set; }

        public double Average { get; set; }

        public double StrikeRate { get; set; }

        public int Highest { get; set; }

        public int Fifties { get; set; }

        public int Hundreds{ get; set; }

        public int Quaters { get; set; }

        public int Fours { get; set; }

        public int Sixes { get; set; }

        public int Outs { get; set; }

        public int NotOuts { get; set; }
    }
}