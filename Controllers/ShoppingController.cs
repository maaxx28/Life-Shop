using Microsoft.AspNetCore.Mvc;

namespace LifeShop.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult ShoppingIndex()
        {
           return View("Shop");
        }
    }
}
