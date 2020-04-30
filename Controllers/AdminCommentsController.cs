using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogApp.Data;
using BlogApp.Entities;

namespace BlogApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminCommentsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: AdminComments

        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Blog);
            return View(comments.ToList());
        }


        // GET: AdminComments/Create
        public ActionResult Create()
        {
            ViewBag.BlogModelId = new SelectList(db.Blogs, "Id", "Title");
            return View();
        }

        // POST: AdminComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Article,When,UserName,isApproved,BlogModelId")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogModelId = new SelectList(db.Blogs, "Id", "Title", comments.BlogModelId);
            return View(comments);
        }


        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogModelId = new SelectList(db.Blogs, "Id", "Title", comments.BlogModelId);
            return View(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Article,UserName,isApproved,BlogModelId")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                comments.When = DateTime.Now;
                comments.BlogModelId = comments.BlogModelId;
                comments.UserName = comments.UserName;
                db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogModelId = new SelectList(db.Blogs, "Id", "Title", comments.BlogModelId);
            return View(comments);
        }







        // GET: AdminComments/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // POST: AdminComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            Comments comments = db.Comments.Find(id);
            db.Comments.Remove(comments);
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
