using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using alanata_zadanie1.Models;

namespace alanata_zadanie1.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string orderBy = "Name", string orderByDir = "ASC", int perPage = 10, int currentPage = 1)
        {
            IQueryable<User> users = db.User;

            users = ApplySorting(users,orderBy,orderByDir == "ASC");

            int numOfPages = (int) Math.Ceiling((double) users.Count() / perPage);
            users = users.Skip((currentPage - 1) * perPage).Take(perPage);
            
            var model = new UserViewModel
            {
                Users = users.ToList(),
                NumOfPages = numOfPages,
                CurrentPage = currentPage,
                PerPage = perPage,
                OrderBy = orderBy,
                OrderByDirection = orderByDir,
                PerPageOptions = new[] { 10, 20, 40 },
                OrderByOptions = Models.User.GetVisibleFields(),
                OrderByDirOptions = new[] { "ASC", "DESC" }
            };

            return View(model);
        }
        private IQueryable<User> ApplySorting(IQueryable<User> users, string orderBy, bool sortAscending)
        {
            var parameter = Expression.Parameter(typeof(User), "u");
            var property = Expression.Property(parameter, orderBy);
            var lambda = Expression.Lambda(property, parameter);

            string methodName = sortAscending ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(User), property.Type }, users.Expression, Expression.Quote(lambda));

            return users.Provider.CreateQuery<User>(resultExpression);
        }
        public ActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            db.User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChangeLocale(string lang)
        {
            HttpCookie cookie = new HttpCookie("Culture", lang)
            {
                Expires = DateTime.Now.AddYears(1)
            };
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}