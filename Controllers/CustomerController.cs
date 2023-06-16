using Microsoft.AspNetCore.Mvc;

namespace LifeShop.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult CustomerDetails()
        {
            return View("CustomerDetails");
        }
    }
}
