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
    public class TaskQueuesController : Controller
    {
        private TaskEntities db = new TaskEntities();

        // GET: TaskQueues
        public ActionResult Index()
        {
            var taskQueues = db.TaskQueues.Include(t => t.Task);
            return View(taskQueues.ToList());
        }

        // GET: TaskQueues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskQueue taskQueue = db.TaskQueues.Find(id);
            if (taskQueue == null)
            {
                return HttpNotFound();
            }
            return View(taskQueue);
        }

        // GET: TaskQueues/Create
        public ActionResult Create()
        {
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName");
            return View();
        }

        // POST: TaskQueues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QueueId,TaskId,Status,Priority,CreatedDate,ExecutionDate")] TaskQueue taskQueue)
        {
            if (ModelState.IsValid)
            {
                db.TaskQueues.Add(taskQueue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskQueue.TaskId);
            return View(taskQueue);
        }

        // GET: TaskQueues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskQueue taskQueue = db.TaskQueues.Find(id);
            if (taskQueue == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskQueue.TaskId);
            return View(taskQueue);
        }

        // POST: TaskQueues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QueueId,TaskId,Status,Priority,CreatedDate,ExecutionDate")] TaskQueue taskQueue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskQueue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskQueue.TaskId);
            return View(taskQueue);
        }

        // GET: TaskQueues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskQueue taskQueue = db.TaskQueues.Find(id);
            if (taskQueue == null)
            {
                return HttpNotFound();
            }
            return View(taskQueue);
        }

        // POST: TaskQueues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskQueue taskQueue = db.TaskQueues.Find(id);
            db.TaskQueues.Remove(taskQueue);
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
