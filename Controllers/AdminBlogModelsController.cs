using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogApp.Data;
using BlogApp.Entities;

namespace BlogApp.Controllers
{
    [Authorize]
    public class AdminBlogModelsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: AdminBlogModels

        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.Category).AsQueryable();
      

            return View(blogs.ToList());
        }


        // GET: AdminBlogModels/Create
        public ActionResult Create()
        {
            ViewBag.CategoryModelId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: AdminBlogModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Article,Description,Url,CategoryModelId")] BlogModel blogModel, HttpPostedFileBase file)
        {  

            if (ModelState.IsValid)
            {
                var path = Server.MapPath("~/Upload/Blogs");
                var filename = Path.ChangeExtension(file.FileName.ToString() + "", ".jpg");

                if (file == null)
                {
                    filename = "place.jpg";
                }
                var fullpath = Path.Combine(path, filename);

                file.SaveAs(fullpath);

                blogModel.Url = filename.ToString();

                blogModel.Author = User.Identity.Name;
                blogModel.When = DateTime.Now;
                blogModel.isApproved = false;
                db.Blogs.Add(blogModel);
                db.SaveChanges();
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }

            ViewBag.CategoryModelId = new SelectList(db.Categories, "Id", "Name", blogModel.CategoryModelId);
            return View(blogModel);
        }

        // GET: AdminBlogModels/Edit/5

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogModel blogModel = db.Blogs.Find(id);
            if (blogModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryModelId = new SelectList(db.Categories, "Id", "Name", blogModel.CategoryModelId);
            return View(blogModel);
        }

        // POST: AdminBlogModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Title,Article,Description,Url,Author,isApproved,CategoryModelId")] BlogModel blogModel)
        {
            if (ModelState.IsValid)
            {
                blogModel.Author = blogModel.Author;
                blogModel.Url = blogModel.Url;
                blogModel.When = DateTime.Now;
                blogModel.Article = blogModel.Article;
                db.Entry(blogModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryModelId = new SelectList(db.Categories, "Id", "Name", blogModel.CategoryModelId);
            return View(blogModel);
        }

        // GET: AdminBlogModels/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogModel blogModel = db.Blogs.Find(id);
            if (blogModel == null)
            {
                return HttpNotFound();
            }
            return View(blogModel);
        }

        // POST: AdminBlogModels/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogModel blogModel = db.Blogs.Find(id);
            db.Blogs.Remove(blogModel);
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
