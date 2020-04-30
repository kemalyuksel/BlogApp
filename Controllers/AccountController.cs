using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Identity;
using BlogApp.Entities;
using Microsoft.AspNetCore.Identity;
using IdentityResult = Microsoft.AspNet.Identity.IdentityResult;
using BlogApp.Data;
using System.IO;

namespace BlogApp.Controllers
{
    public class AccountController : Controller
    {

        DataContext db = new DataContext();

        private Microsoft.AspNet.Identity.UserManager<ApplicationUser> userManager;
        private Microsoft.AspNet.Identity.RoleManager<ApplicationRole> roleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new Identity.IdentityDbContext());
            userManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new Identity.IdentityDbContext());
            roleManager = new Microsoft.AspNet.Identity.RoleManager<ApplicationRole>(roleStore);
        }

        [Authorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = userManager.FindByName(User.Identity.Name);
                return View(user);
            }
            else
            {
                return View();
            }

        }


        public PartialViewResult ProfileTable()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var blogs = db.Blogs.Where(x=> x.Author == id.ToString()).ToList();


            return PartialView(blogs);
        }



        // GET: Account
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model, HttpPostedFileBase file)
        {
          


            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var path = Server.MapPath("~/Upload/Users"); //dosya yolu
                    var filename = Path.ChangeExtension(model.Username + "", ".jpg");

                    if (file == null)
                    {
                        filename = "place.jpg";
                    }
                    var fullpath = Path.Combine(path, filename);

                    file.SaveAs(fullpath);

                    model.UserImage = filename.ToString();

                    //Kayıt İşlemleri

                    ApplicationUser user = new ApplicationUser();

                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Email = model.Email;
                    user.UserName = model.Username;

                    IdentityResult result = userManager.Create(user, model.Password);

                    if (result.Succeeded)
                    {
                        //Kullanıcı oluştu ve rol atanabilir.


                        if (roleManager.RoleExists("user"))
                        {

                            userManager.AddToRole(user.Id, "user");
                        }

                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturulamadı.");
                    }


                }



                return View(model);

            }


        }


        // GET: Account
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                //Login işlemleri

                var user = userManager.Find(model.Username, model.Password);

                if (user != null)
                {
                    //Varolan kullanıcı sisteme dahil et  
                    // AppCookie oluşturup sisteme bırak.

                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identitycalims = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties, identitycalims);

                    var role = user.Roles.ToString();

                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    return RedirectToAction("Index", "Home");


                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Giriş Başarısız.");
                }


            }


            return View(model);
        }

        // GET: Account

        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();


            return RedirectToAction("Index", "Home");
        }




    }
}