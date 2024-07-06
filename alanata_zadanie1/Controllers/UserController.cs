using System.Linq;
using System.Web.Mvc;
using alanata_zadanie1.Models;

namespace alanata_zadanie1.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET
        public ActionResult Index()
        {
            ViewBag.Title = "User evidence";
            return View(db.User.ToList());
        }
    }
}