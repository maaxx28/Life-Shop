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
            }
            else
            {
                HttpContext.Session.SetInt32("_LoggedInCustomerID", theID);
            }
            return Index();
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