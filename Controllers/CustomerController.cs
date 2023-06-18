using LifeShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace LifeShop.Controllers
{
    public class CustomerController : Controller
    {


        public IActionResult CustomerDetails(int id)
        {
            int loggedInCustomer = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if (loggedInCustomer == 0)
            {
                return View("Logon");
            }
            else
            {
                ViewData["LoggedIn"] = 1;
                ViewBag.CurrentUser = new Customer(loggedInCustomer);
            }
            ViewBag.currentDetails = new Models.Customer(id);
            
            return View("CustomerDetails");
        }
    }
}
