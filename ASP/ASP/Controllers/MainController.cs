using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.Models;
using ASP.DataAbstractionLayer;

namespace ASP.Controllers
{
    public class MainController : Controller
    {
        private DAL dal;

        public MainController()
        {
            dal = new DAL();
        }
        
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            User user;
            string username = Request.Params["usernameLogin"];
            string password = Request.Params["passwordLogin"];
            user = dal.Login(username, password);
            if (user == null)
            {
                return View("Error");
            }
            else
            {
                Session["user"] = username;
                return RedirectToAction("Home");
            }
        }

        public ActionResult Home()
        {
            List<Book> books = dal.GetBooks();
            ViewData["bookList"] = books;
            return View("Home");
        }

        public ActionResult Logout()
        {
            Session.Contents.Remove("user");
            return RedirectToAction("Index");
        }

        public ActionResult Error()
        {
            return RedirectToAction("Index");
        }

        public ActionResult GoToSaveBook()
        {
            return View("Save");
        }
        
        public ActionResult SaveBook()
        {
            Book book = new Book();
            book.title = Request.Params["inputTitle_insert"];
            book.author = Request.Params["inputAuthor_insert"];
            book.pages = Int32.Parse(Request.Params["inputPages_insert"]);
            book.price = Int32.Parse(Request.Params["inputPrice_insert"]);
            
            dal.saveBook(book);
            
            return RedirectToAction("Home");
        }

        public ActionResult DeleteBook()
        {
            string titleToDelete = Request.Params["titleDelete"];
            dal.deleteBook(titleToDelete);
            
            return RedirectToAction("Home");
        }

        public ActionResult UpdateBook()
        {
            Book book = new Book();
            int id = dal.getBookId(Request.Params["currentTitle"]);
            
            book.title = Request.Params["inputTitle_update"];
            book.author = Request.Params["inputAuthor_update"];
            book.pages = Int32.Parse(Request.Params["inputPages_update"]);
            book.price = Int32.Parse(Request.Params["inputPrice_update"]);
            
            dal.updateBook(id, book);
            return RedirectToAction("Home");
        }

        public ActionResult LendBook()
        {
            int id = dal.getBookId(Request.Params["currentTitle"]);
            string lender = (String)Session["user"];
            Console.WriteLine(lender);
            Console.WriteLine(id);
            
            dal.lend(id, lender);
            
            return RedirectToAction("Home");
        }

        public ActionResult GoToFilter()
        {
            return View("Filter");
        }
        
        public string FilterBooks()
        {
            string author = Request.Params["inputFilter"];
            List<Book> books = dal.filter(author);
            
            string result = "<table><thead><th>Title</th><th>Author</th><th>Pages</th><th>Price</th><th>Lender</th></thead>";

            foreach (Book book in books)
            {
                result += "<tr><td>" + book.title + "</td><td>" + book.author + "</td><td>" + book.pages + "</td><td>" + book.price + "</td><td>"+ book.lender +"</td></tr>";
            }

            result += "</table>";
            return result;
        }

        public ActionResult GoToRegister()
        {
            return View("Register");
        }

        public ActionResult Register()
        {
            string username = Request.Params["usernameRegister"];
            string password = Request.Params["passwordRegister"];
            
            dal.Register(username, password);
            return RedirectToAction("Index");
        }
        
        
    }
}