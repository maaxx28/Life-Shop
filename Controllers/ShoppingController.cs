using LifeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace LifeShop.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult ShoppingIndex()
        {
           List<Product> shopItems = Models.Product.GetList();
            ViewBag.shopItems = shopItems;
           return View("Shop");
        }
    }
}
