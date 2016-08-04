using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seagulls.Models
{
    public class ScoreViewModel
    {
        public virtual int MatchId { get; set; }
        public virtual ICollection<BattingScoreCard> BattingScoreCards { get; set; }
        public virtual ICollection<BowlingScoreCard> BowlingScoreCards { get; set; }
    }
}