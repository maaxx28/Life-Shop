using LifeShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LifeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int loggedInCustomer = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;
            if (loggedInCustomer == 0)
            {
                
            }
            else
            {
                ViewData["LoggedIn"] = 1;
                ViewBag.CurrentUser = new Customer(loggedInCustomer);
            }
            return View("Index");
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            int theID = Models.Customer.ValidateLogin(HttpContext.Request.Form["Username"], HttpContext.Request.Form["Password"]);
            if (theID == 0)
            {
                ViewData["Message"] = "Login Failed! Please Try again!";
                return View("Logon");
            }
            else
            {
                HttpContext.Session.SetInt32("_LoggedInCustomerID", theID);
                return Index();
            }
           // return Index();
        }
        public IActionResult Logon()
        {
            return View("Logon");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.SetInt32("_LoggedInCustomerID", 0);
            ViewData["Message"] = "Successfully Logged Out.";
            return Index();
        }

        public ActionResult AddCustomer()
        {
            Customer newCustomer = new(0);
            newCustomer.FirstName = HttpContext.Request.Form["addFirstName"];
            newCustomer.LastName = HttpContext.Request.Form["addLastName"];
            newCustomer.Address = HttpContext.Request.Form["addAddress"];
            newCustomer.City = HttpContext.Request.Form["addCity"];
            newCustomer.State = HttpContext.Request.Form["addState"];
            newCustomer.Zip = HttpContext.Request.Form["addZip"];
            newCustomer.Email = HttpContext.Request.Form["addEmail"];
            newCustomer.Phone = HttpContext.Request.Form["addPhone"];
            newCustomer.UserName = HttpContext.Request.Form["addUserName"];
            newCustomer.Password = HttpContext.Request.Form["addPassword"];

            string availableUserName = Models.Customer.ValidUserName(HttpContext.Request.Form["addUserName"].ToString());  

            if (HttpContext.Request.Form["addUserName"].ToString() == "")
            {
                ViewData["NewCustomerMessage"] = "Username cannot be null.";
                return View("Register");
            }
            else if (HttpContext.Request.Form["addUserName"].ToString()== availableUserName)
            {
                ViewData["NewCustomerMessage"] = "Username is not available";
                return View("Register");
            }
            else if (HttpContext.Request.Form["addPassword"].ToString() == "")
            {
                ViewData["NewCustomerMessage"] = "Password must be filled out";
                return View("Register");
            }
            else
            {
                newCustomer.Save();
                int theID = Models.Customer.ValidateLogin(HttpContext.Request.Form["addUserName"], HttpContext.Request.Form["addPassword"]);
                HttpContext.Session.SetInt32("_LoggedInCustomerID", theID);
                //CustomerDetails(newCustomer.ID);
                ViewData["CustomerEditResult"] = "Customer Created Successfully!";
                Index();
                return View("Index");
            }

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}