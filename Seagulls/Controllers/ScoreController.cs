using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Seagulls.Models;

namespace Seagulls.Controllers
{
    public class ScoreController : Controller
    {
        private SeagullsEntities db = new SeagullsEntities();

        //
        // GET: /Score/
        public ActionResult Index(int matchId)
        {
            ScoreViewModel scoreViewModel = new ScoreViewModel();
            if (matchId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scoreViewModel.BattingScoreCards = db.BattingScoreCards.Where(p => p.MatchId == matchId).ToList<BattingScoreCard>();
            scoreViewModel.BowlingScoreCards = db.BowlingScoreCards.Where(p => p.MatchId == matchId).ToList<BowlingScoreCard>();
            scoreViewModel.MatchId = matchId;
            return View(scoreViewModel);
        }
    }
}
