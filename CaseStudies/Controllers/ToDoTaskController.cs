using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaseStudies.Models;

namespace CaseStudies.Controllers
{
    public class ToDoTaskController : Controller
    {
        private ToDoContext db = new ToDoContext();

        // GET: /ToDoTask/
        public ActionResult Index()
        {
            return View(db.Tasks.Where(n => n.UserId == HttpContext.User.Identity.Name).ToList());
        }

        // GET: /ToDoTask/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoTask todotask = db.Tasks.Find(id);
            if (todotask == null)
            {
                return HttpNotFound();
            }
            return View(todotask);
        }

        // GET: /ToDoTask/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ToDoTask/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,Description,DueDate")] ToDoTask todotask)
        {
            if (ModelState.IsValid)
            {
                todotask.CreateDate = DateTime.Now;
                todotask.UserId = HttpContext.User.Identity.Name;

                db.Tasks.Add(todotask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todotask);
        }

        // GET: /ToDoTask/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoTask todotask = db.Tasks.Find(id);
            if (todotask == null)
            {
                return HttpNotFound();
            }
            return View(todotask);
        }

        // POST: /ToDoTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,UserId,Name,Description,DueDate,CreateDate")] ToDoTask todotask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todotask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todotask);
        }

        // GET: /ToDoTask/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoTask todotask = db.Tasks.Find(id);
            if (todotask == null)
            {
                return HttpNotFound();
            }
            return View(todotask);
        }

        // POST: /ToDoTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoTask todotask = db.Tasks.Find(id);
            db.Tasks.Remove(todotask);
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
