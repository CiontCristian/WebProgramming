using System.Web.Mvc;
using ASP.DataAbstractionLayer;
using ExamModel1.Models;

namespace ExamModel1.Controllers
{
    public class MainController : Controller
    {
        private DAL dal;

        public MainController()
        {
            dal = new DAL();
        }
        // GET
        public ActionResult Index()
        {
            return View("Login2");
        }

        public ActionResult Login()
        {
            Person user;
            string name = Request.Params["name"];
            user = dal.Login(name);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["user"] = user;
                return RedirectToAction("Home");
            }
        }

        public ActionResult Home()
        {
            return View("Home2");
        }
    }
}