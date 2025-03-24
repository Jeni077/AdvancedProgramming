using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdvancedProgramming.Data;

namespace AdvancedProgramming.Controllers
{
    public class TaskHistoriesController : Controller
    {
        private TaskEntities db = new TaskEntities();

        // GET: TaskHistories
        public ActionResult Index()
        {
            var taskHistories = db.TaskHistories.Include(t => t.Task);
            return View(taskHistories.ToList());
        }

        // GET: TaskHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskHistory taskHistory = db.TaskHistories.Find(id);
            if (taskHistory == null)
            {
                return HttpNotFound();
            }
            return View(taskHistory);
        }

        // GET: TaskHistories/Create
        public ActionResult Create()
        {
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName");
            return View();
        }

        // POST: TaskHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoryId,TaskId,Status,ExecutionStart,ExecutionEnd")] TaskHistory taskHistory)
        {
            if (ModelState.IsValid)
            {
                db.TaskHistories.Add(taskHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskHistory.TaskId);
            return View(taskHistory);
        }

        // GET: TaskHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskHistory taskHistory = db.TaskHistories.Find(id);
            if (taskHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskHistory.TaskId);
            return View(taskHistory);
        }

        // POST: TaskHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoryId,TaskId,Status,ExecutionStart,ExecutionEnd")] TaskHistory taskHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskHistory.TaskId);
            return View(taskHistory);
        }

        // GET: TaskHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskHistory taskHistory = db.TaskHistories.Find(id);
            if (taskHistory == null)
            {
                return HttpNotFound();
            }
            return View(taskHistory);
        }

        // POST: TaskHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskHistory taskHistory = db.TaskHistories.Find(id);
            db.TaskHistories.Remove(taskHistory);
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
