using Seagulls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seagulls.Controllers
{
    public class StatsController : Controller
    {
        private SeagullsEntities db = new SeagullsEntities();

        private List<BowlingStatsViewModel> LoadBowlingStats()
        {
            var v = from row in db.BowlingScoreCards
                    group row by row.PlayerId into p
                    select new BowlingStatsViewModel
                    {
                        Id = p.Key,
                        InningsCount = p.Count(),
                        Name = (from pl in db.Players
                                where pl.Id == p.Key
                                select pl).FirstOrDefault().FirstName,
                        Runs = p.Sum(s => s.Runs),
                        Overs = p.Sum(s => Math.Truncate(s.Overs)) + Math.Truncate((p.Sum(b => (decimal)(b.Overs - (int)b.Overs)) * 10) / 6),
                        Balls = (p.Sum(b => (decimal)(b.Overs - (int)b.Overs)) * 10) % 6, //- (int)p.Sum(o => Math.Round(o.Overs, 2))),
                        Wides = p.Sum(f => f.Wides),
                        NoBalls = p.Sum(s => s.NoBalls),
                        ThreeWickets = p.Where(r => r.Wickets > 2 && r.Wickets < 5).Count(),
                        FiveWickets = p.Where(r => r.Wickets > 4).Count(),
                        Wickets = p.Sum(r => r.Wickets),
                        BestWickets = p.OrderByDescending(r => r.Wickets).FirstOrDefault().Wickets,
                        BestRuns = p.OrderByDescending(r => r.Wickets).FirstOrDefault().Runs,
                        //StrikeRate = (p.Sum(b => b.Wickets) > 0)?p.Sum(s => s.Runs) / p.Sum(b => b.Wickets): 0,
                        RunsPerOver = (p.Sum(s => s.Overs) > 0) ? p.Sum(s => s.Runs) / p.Sum(s => s.Overs) : 0
                    };

            return v.OrderByDescending(p => p.Wickets).ToList();
        }

        private List<BattingStatsViewModel> LoadBattingStats()
        {
            var v = from row in db.BattingScoreCards
                    group row by row.PlayerId into p
                    select new BattingStatsViewModel
                    {
                        Id = p.Key,
                        Name = (from pl in db.Players
                                where pl.Id == p.Key
                                select pl).FirstOrDefault().FirstName,
                        Runs = p.Sum(s => s.RunsScored),
                        Balls = p.Sum(b => b.BallsFaced),
                        Fours = p.Sum(f => f.NoOfFours),
                        Sixes = p.Sum(s => s.NoOfSix),
                        Fifties = p.Where(r => r.RunsScored > 49 && r.RunsScored < 100).Count(),
                        Hundreds = p.Where(r => r.RunsScored > 99).Count(),
                        Quaters = p.Where(r => r.RunsScored > 24 && r.RunsScored < 50).Count(),
                        Highest = p.OrderByDescending(r => r.RunsScored).FirstOrDefault().RunsScored,
                        StrikeRate = Math.Round(((double)p.Sum(s => s.RunsScored) / p.Sum(b => b.BallsFaced)) * 100,2),
                        InningsCount = p.Count(),
                        Average = Math.Round((p.Where(x => x.HowOut.ToUpper() != "NOT OUT" && x.HowOut.ToUpper() != "RET HURT").Count() > 0) ? (double)p.Sum(s => s.RunsScored) / p.Where(x => x.HowOut.ToUpper() != "NOT OUT" && x.HowOut.ToUpper() != "RET HURT").Count() : 0,2),
                        NotOuts = (from r in p
                                   where r.HowOut.ToUpper() == "NOT OUT" || r.HowOut.ToUpper() == "RET HURT"
                                   select r).Count(),
                        Outs = (from r in p
                                where r.HowOut.ToUpper() != "NOT OUT" && r.HowOut.ToUpper() != "RET HURT"
                                select r).Count(),
                    };
            return v.OrderByDescending(p => p.Runs).ToList();
        }
        // GET: Stats
        public ActionResult Index()
        {
            StatsViewModel stats = new StatsViewModel();

            stats.BattingStats = LoadBattingStats();
            stats.BowlingStats = LoadBowlingStats();
            
            return View(stats);          
        }
    }
}