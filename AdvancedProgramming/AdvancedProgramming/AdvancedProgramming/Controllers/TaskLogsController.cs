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
    public class TaskLogsController : Controller
    {
        private TaskEntities db = new TaskEntities();

        // GET: TaskLogs
        public ActionResult Index()
        {
            var taskLogs = db.TaskLogs.Include(t => t.Task);
            return View(taskLogs.ToList());
        }

        // GET: TaskLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskLog taskLog = db.TaskLogs.Find(id);
            if (taskLog == null)
            {
                return HttpNotFound();
            }
            return View(taskLog);
        }

        // GET: TaskLogs/Create
        public ActionResult Create()
        {
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName");
            return View();
        }

        // POST: TaskLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogId,TaskId,LogMessage,CreatedDate")] TaskLog taskLog)
        {
            if (ModelState.IsValid)
            {
                db.TaskLogs.Add(taskLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskLog.TaskId);
            return View(taskLog);
        }

        // GET: TaskLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskLog taskLog = db.TaskLogs.Find(id);
            if (taskLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskLog.TaskId);
            return View(taskLog);
        }

        // POST: TaskLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogId,TaskId,LogMessage,CreatedDate")] TaskLog taskLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskLog.TaskId);
            return View(taskLog);
        }

        // GET: TaskLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskLog taskLog = db.TaskLogs.Find(id);
            if (taskLog == null)
            {
                return HttpNotFound();
            }
            return View(taskLog);
        }

        // POST: TaskLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskLog taskLog = db.TaskLogs.Find(id);
            db.TaskLogs.Remove(taskLog);
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
