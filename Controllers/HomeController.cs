using BlogApp.Data;
using BlogApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();


        public ActionResult Index(int? id,string key)
        {
            var blogs = db.Blogs.Where(x=> x.isApproved == true).OrderByDescending(x => x.When).AsQueryable();

            if (id != null)
            {
                blogs = blogs.Where(i => i.Category.Id == id).AsQueryable();
            }
            if(key != null)
            {
                blogs = blogs.Where(i => i.Article.Contains(key) | i.Title.Contains(key) | i.Category.Name.Contains(key) | i.Author.Contains(key)).AsQueryable();
            }

            



            return View(blogs.ToList());
        }



        [HttpGet]
        public ActionResult AddComments()
         {
            ViewBag.BlogsId = new SelectList(db.Blogs, "Id");
            return View();
         }

    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComments([Bind(Include = "Id,Article")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                var id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
                comments.BlogModelId = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
                comments.When = DateTime.Now;
                comments.UserName = User.Identity.Name;
                comments.isApproved = false;
                db.Comments.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Single/" + id);
            }

            ViewBag.BlogModelId = new SelectList(db.Blogs, "Id", "Name", comments.BlogModelId);
            return View(comments);
        }


        public PartialViewResult GetComments()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var comms = db.Comments.AsEnumerable().Where(x => x.BlogModelId == int.Parse(id.ToString()) && x.isApproved == true).ToList();

            ViewBag.yorum = comms.Count();

            return PartialView(comms);
        }

        public PartialViewResult GetCategories()
        {
            var ctgs = db.Categories.ToList();

            return PartialView(ctgs);
        }



        public ActionResult Likes(int? id)
        {
            if(id!= null)
            {
                var sing = db.Blogs.Find(id);
                sing.Likes += 1;
                db.SaveChanges();
                return RedirectToAction("Single/"+id, "home");
            }
            return RedirectToAction("Single/" + id, "home");
        }

        public PartialViewResult LatestBlogs()
        {

            return PartialView(db.Blogs.Where(x => x.isApproved == true ).OrderByDescending(x => x.When).Take(3).ToList());
        }

        public PartialViewResult HotBlogs()
        {

            return PartialView(db.Blogs.Where(x => x.isApproved == true ).OrderByDescending(x => x.Looks).Take(3).ToList());
        }

        public ActionResult Single(int? id)
        {
            var sing = db.Blogs.Find(id);
            if(sing.isApproved == true)
            {
                sing.Looks += 1;
                db.SaveChanges();
                return View(sing);
            }
            return RedirectToAction("Index","Home");

        }

        public ActionResult List(int? id,string key)
        {
            var list = db.Blogs.Where(x => x.isApproved == true).OrderByDescending(x => x.When).AsQueryable();
            

            if (id != null)
            {
                list = list.Where(i => i.Category.Id == id).AsQueryable();
            }
            if (key != null)
            {
                list = list.Where(i => i.Article.Contains(key) | i.Title.Contains(key) | i.Category.Name.Contains(key) | i.Author.Contains(key)).AsQueryable();
            }

            return View(list.ToList());
        }


    }
}