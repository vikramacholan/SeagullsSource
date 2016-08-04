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
    public class MatchController : Controller
    {
        private SeagullsEntities db = new SeagullsEntities();

        // GET: /Match/
        [AllowAnonymous]
        public ActionResult Index()
        {
            var matches = db.Matches.Include(m => m.League).Include(m => m.Venu);
            return View(matches.OrderByDescending(m => m.MatchDate).ToList());
        }

        // GET: /Match/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: /Match/Create
        public ActionResult Create()
        {
            ViewBag.LeagueId = new SelectList(db.Leagues, "Id", "Name");
            ViewBag.VenuId = new SelectList(db.Venus, "Id", "Description");
            ViewBag.CaptainId = new SelectList(db.Players, "Id", "FirstName");
            ViewBag.WicketKeeperId = new SelectList(db.Players, "Id", "FirstName");
            return View();
        }

        // POST: /Match/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include="Id,LeagueId,MatchDate,Opponent,VenuId,Result,TotalScore,OpponentScore,CaptainId,WicketKeeperId,ScoreSheetUrl,LastupdatedBy,LastupdatedDate")] 
        public ActionResult Create(Match match)
        {
            if (ModelState.IsValid)
            {
                match.LastupdatedBy = "Admin";
                match.LastupdatedDate = DateTime.Now;
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LeagueId = new SelectList(db.Leagues, "Id", "Name", match.LeagueId);
            ViewBag.VenuId = new SelectList(db.Venus, "Id", "Description", match.VenuId);
            ViewBag.CaptainId = new SelectList(db.Players, "Id", "FirstName", match.CaptainId);
            ViewBag.WicketKeeperId = new SelectList(db.Players, "Id", "FirstName", match.WicketKeeperId);
            return View(match);
        }

        // GET: /Match/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeagueId = new SelectList(db.Leagues, "Id", "Name", match.LeagueId);
            ViewBag.VenuId = new SelectList(db.Venus, "Id", "Description", match.VenuId);
            return View(match);
        }

        // POST: /Match/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,LeagueId,MatchDate,Opponent,VenuId,Result,TotalScore,OpponentScore,CaptainId,WicketKeeperId,ScoreSheetUrl,LastupdatedBy,LastupdatedDate")] Match match)
        {
            if (ModelState.IsValid)
            {
                match.LastupdatedBy = "Admin";
                match.LastupdatedDate = DateTime.Now;
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeagueId = new SelectList(db.Leagues, "Id", "Name", match.LeagueId);
            ViewBag.VenuId = new SelectList(db.Venus, "Id", "Description", match.VenuId);
            return View(match);
        }

        // GET: /Match/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: /Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
            db.SaveChanges();
            return RedirectToAction("Index");
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
