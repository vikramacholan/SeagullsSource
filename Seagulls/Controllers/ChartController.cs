using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seagulls.Controllers
{
    [AllowAnonymous]
    public class ChartController : Controller
    {
        private SeagullsEntities db = new SeagullsEntities();

        // GET: Chart
        public ActionResult Index()
        {
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName");
            return View();
        }
        

        public JsonResult LoadChartById(int playerId)
        {            
            var batResult = from row in db.BattingScoreCards 
                         join m in db.Matches on row.MatchId equals m.Id 
                         where row.PlayerId == playerId
                         select new {m.Opponent, row.RunsScored};

            var bowlResult = from row in db.BowlingScoreCards
                         join m in db.Matches on row.MatchId equals m.Id
                         where row.PlayerId == playerId
                         select new { m.Opponent, row.Wickets };


            return Json(new { Batting = batResult.ToArray(), Bowling = bowlResult.ToArray() }, JsonRequestBehavior.AllowGet);
        }
    }
}