using Microsoft.AspNetCore.Mvc;

namespace LifeShop.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Careers()
        {
            return View("Careers");
        }
    }
}
