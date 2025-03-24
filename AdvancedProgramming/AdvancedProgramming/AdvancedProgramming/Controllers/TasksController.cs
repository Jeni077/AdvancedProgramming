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
    public class TasksController : Controller
    {
        private TaskEntities db = new TaskEntities();

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = db.Tasks.Include(t => t.User).OrderBy(t => t.Priority); // Ordenar por prioridad
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskId,TaskName,Description,Priority,Status,ExecutionDate,CreatedDate,UpdatedDate,UserId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", task.UserId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", task.UserId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskId,TaskName,Description,Priority,Status,ExecutionDate,CreatedDate,UpdatedDate,UserId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", task.UserId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RetryTask(int id)
        {
            var task = db.Tasks.Find(id);
            if (task != null && task.Status == "Fallida")
            {
                task.Status = "Pendiente";  // Cambiar el estado a pendiente
                db.SaveChanges();

                // Reinsertar en la cola de ejecución (si es necesario, puedes agregar más lógica aquí)
                ExecuteTask(task);  // Ejecutar la tarea manualmente después de marcarla como pendiente
            }

            return RedirectToAction("Index");
        }


        // Función para ejecutar la tarea
        private void ExecuteTask(Task task)
        {
            try
            {
                task.Status = "En Proceso"; // Marcar la tarea como en proceso
                db.SaveChanges();

                // Simula la ejecución de la tarea (aquí deberías poner la lógica real de la tarea)
                System.Threading.Thread.Sleep(5000); // Simula un trabajo de 5 segundos

                task.Status = "Finalizada"; // Marcar la tarea como finalizada
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Si ocurre un error durante la ejecución, marcar como fallida
                task.Status = "Fallida";
                db.SaveChanges();
            }
        }

        // Acción para manejar la cola de tareas con prioridad
        public ActionResult ProcessTasks()
        {
            var tasks = db.Tasks.Where(t => t.Status == "Pendiente")
                                 .OrderBy(t => t.Priority)  // Ordenar por prioridad (las de mayor prioridad primero)
                                 .ThenBy(t => t.ExecutionDate) // Si tienen la misma prioridad, por la fecha de ejecución
                                 .ToList();

            foreach (var task in tasks)
            {
                // Aquí debes enviar la tarea a un worker o proceso que la ejecute
                task.Status = "En Proceso";
                db.SaveChanges();

                // Llama a la función de ejecución (simulada en este caso)
                ExecuteTask(task);
            }

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
