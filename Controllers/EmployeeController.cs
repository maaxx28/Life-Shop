using LifeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LifeShop.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Careers()
        {
            return View("Careers");
        }
        public IActionResult EmployeeLogin()
        {
            return View("EmployeePortal");
        }
        public IActionResult EmployeeSignIn()
        {
            int theID = Models.Employee.ValidateLogin(HttpContext.Request.Form["Username"], HttpContext.Request.Form["Password"]);
            if (theID == 0)
            {
                ViewData["Message"] = "Login Failed! Please Try again!";
                return View("EmployeePortal");
            }
            else
            {
                HttpContext.Session.SetInt32("_LoggedInEmployeeID", theID);
                return EmployeeTools();
                //return View("EmployeeTools");
            }
        }
        public IActionResult EmployeeTools()
        {
            int loggedInID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            ViewData["LoggedInEmployee"] = 0;
            if (loggedInID == 0) 
            {
                ViewData["Message"] = "You need to Sign in";
                return View("EmployeeTools");
            }
            else
            {

                ViewData["LoggedInEmployee"] = 1;
                ViewBag.CurrentEmply = new Employee(loggedInID);
                return View("EmployeeTools");
            }
        }
        public IActionResult EmployeeSignout() 
        {
            HttpContext.Session.SetInt32("_LoggedInEmployeeID", 0);
            ViewData["Message"] = "Successfully Logged Out.";
            return View("Careers");
        }
    }
}
