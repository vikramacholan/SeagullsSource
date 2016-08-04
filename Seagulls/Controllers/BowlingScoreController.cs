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
    public class BowlingScoreController : Controller
    {
        private SeagullsEntities db = new SeagullsEntities();

     

        // GET: /BowlingScore/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BowlingScoreCard bowlingscorecard = db.BowlingScoreCards.Find(id);
            if (bowlingscorecard == null)
            {
                return HttpNotFound();
            }
            return View(bowlingscorecard);
        }

        // GET: /BowlingScore/Create
        public ActionResult Create(int matchId)
        {
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName");
            BowlingScoreCard scoreCard = new BowlingScoreCard();
            scoreCard.MatchId = matchId;
            return View(scoreCard);
        }

        // POST: /BowlingScore/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MatchId,PlayerId,Overs,Runs,Wickets,Maidens,Wides,NoBalls,RunPerOver,StrikeRate,LastupdatedBy,LastupdatedDate")] BowlingScoreCard bowlingscorecard)
        {
            if (ModelState.IsValid)
            {
                bowlingscorecard.RunPerOver = bowlingscorecard.Runs / bowlingscorecard.Overs;
                bowlingscorecard.StrikeRate = 0;
                bowlingscorecard.LastupdatedBy = "Admin";
                bowlingscorecard.LastupdatedDate = DateTime.Now;
                db.BowlingScoreCards.Add(bowlingscorecard);

                db.SaveChanges();
                return RedirectToAction("Index", "Score", new { matchId = bowlingscorecard.MatchId });
            }

            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName", bowlingscorecard.PlayerId);
            return View(bowlingscorecard);
        }

        // GET: /BowlingScore/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BowlingScoreCard bowlingscorecard = db.BowlingScoreCards.Find(id);
            if (bowlingscorecard == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName", bowlingscorecard.PlayerId);
            return View(bowlingscorecard);
        }

        // POST: /BowlingScore/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MatchId,PlayerId,Overs,Runs,Wickets,Maidens,Wides,NoBalls,RunPerOver,StrikeRate,LastupdatedBy,LastupdatedDate")] BowlingScoreCard bowlingscorecard)
        {
            if (ModelState.IsValid)
            {
                bowlingscorecard.RunPerOver = bowlingscorecard.Runs / bowlingscorecard.Overs;
                bowlingscorecard.StrikeRate = 0;
                bowlingscorecard.LastupdatedBy = "Admin";
                bowlingscorecard.LastupdatedDate = DateTime.Now;
                db.Entry(bowlingscorecard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Score", new { matchId = bowlingscorecard.MatchId });
            }
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName", bowlingscorecard.PlayerId);
            return View(bowlingscorecard);
        }

        // GET: /BowlingScore/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BowlingScoreCard bowlingscorecard = db.BowlingScoreCards.Find(id);
            if (bowlingscorecard == null)
            {
                return HttpNotFound();
            }
            return View(bowlingscorecard);
        }

        // POST: /BowlingScore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BowlingScoreCard bowlingscorecard = db.BowlingScoreCards.Find(id);
            db.BowlingScoreCards.Remove(bowlingscorecard);
            db.SaveChanges();
            return RedirectToAction("Index", "Score", new { matchId = bowlingscorecard.MatchId });
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
