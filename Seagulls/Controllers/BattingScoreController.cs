using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Seagulls;

namespace Seagulls.Controllers
{
    [Authorize]
    public class BattingScoreController : Controller
    {
        private SeagullsEntities db = new SeagullsEntities();

    
        // GET: /BattingScore/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BattingScoreCard battingscorecard = db.BattingScoreCards.Find(id);
            if (battingscorecard == null)
            {
                return HttpNotFound();
            }
            return View(battingscorecard);
        }

        // GET: /BattingScore/Create
        public ActionResult Create(int matchId)
        {
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName");
            BattingScoreCard scoreCard = new BattingScoreCard();
            scoreCard.MatchId = matchId;
            return View(scoreCard);
        }

        // POST: /BattingScore/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MatchId,PlayerId,RunsScored,BallsFaced,HowOut,NoOfFours,NoOfSix,StrikeRate,LastupdatedBy,LastupdatedDate")] BattingScoreCard battingscorecard)
        {
            if (ModelState.IsValid)
            {
                battingscorecard.StrikeRate = ((decimal)battingscorecard.RunsScored / (decimal)battingscorecard.BallsFaced) * 100;
                battingscorecard.LastupdatedBy = "Admin";
                battingscorecard.LastupdatedDate = DateTime.Now;
                db.BattingScoreCards.Add(battingscorecard);
                db.SaveChanges();
                return RedirectToAction("Index", "Score", new { matchId = battingscorecard.MatchId });
            }

            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName", battingscorecard.PlayerId);
            return View(battingscorecard);
        }

        // GET: /BattingScore/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BattingScoreCard battingscorecard = db.BattingScoreCards.Find(id);
            if (battingscorecard == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName", battingscorecard.PlayerId);
            return View(battingscorecard);
        }

        // POST: /BattingScore/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MatchId,PlayerId,RunsScored,BallsFaced,HowOut,NoOfFours,NoOfSix,StrikeRate,LastupdatedBy,LastupdatedDate")] BattingScoreCard battingscorecard)
        {
            if (ModelState.IsValid)
            {
                battingscorecard.StrikeRate = ((decimal)battingscorecard.RunsScored / (decimal)battingscorecard.BallsFaced) * 100;
                battingscorecard.LastupdatedBy = "Admin";
                battingscorecard.LastupdatedDate = DateTime.Now;
                db.Entry(battingscorecard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Score", new { matchId = battingscorecard.MatchId });
            }
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName", battingscorecard.PlayerId);
            return View(battingscorecard);
        }

        // GET: /BattingScore/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BattingScoreCard battingscorecard = db.BattingScoreCards.Find(id);
            if (battingscorecard == null)
            {
                return HttpNotFound();
            }
            return View(battingscorecard);
        }

        // POST: /BattingScore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BattingScoreCard battingscorecard = db.BattingScoreCards.Find(id);
            db.BattingScoreCards.Remove(battingscorecard);
            db.SaveChanges();
            return RedirectToAction("Index", "Score", new { matchId = battingscorecard.MatchId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
