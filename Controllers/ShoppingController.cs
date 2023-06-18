using LifeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace LifeShop.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult ShoppingIndex()
        {
            int EmplyLoggedInID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            int CustLoggedInID = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;

            ViewData["LoggedInEmployee"] = 0;
            if (EmplyLoggedInID != 0)
            {
                ViewData["ProductAdmin"] = 1;

            }
            ViewData["CustomerShopping"] = 0;
            if (CustLoggedInID != 0)
            {
                ViewData["CustomerShopping"] = 1;
            }
            List<Product> shopItems = Models.Product.GetList();
            ViewBag.shopItems = shopItems;
            return View("Shop");
        }
        public IActionResult NewProduct()
        {
            return View("NewProduct");
        }
        public IActionResult AddProduct()
        {


            if (HttpContext.Request.Form["addName"].ToString() == "")
            {
                ViewData["Message"] = "Name must be entered";
                return View("NewProduct");
            }
            else if (HttpContext.Request.Form["addPrice"].ToString() == "")
            {
                ViewData["Message"] = "Price Must Be Entered";
                return View("NewProduct");
            }
            else
            {
                Product newProduct = new Product(0);
                newProduct.Name = HttpContext.Request.Form["addName"];
                newProduct.Description = HttpContext.Request.Form["AddDesc"];
                newProduct.Price = Convert.ToDecimal(HttpContext.Request.Form["AddPrice"]);
                newProduct.Discount = Convert.ToInt32(HttpContext.Request.Form["addDiscount"]);
                newProduct.Picture = HttpContext.Request.Form["addPicture"];


                newProduct.Save();
                ShoppingIndex();
                return View("Shop");
            }
        }
        public IActionResult DeleteProduct(int id) 
        {
            Models.Product.Delete(id);
            ShoppingIndex();
            return View("Shop");
        }
        public IActionResult ViewDetails(int id)
        {
            Product thisProduct = new(id);
            ViewBag.thisProduct = thisProduct;
            return View("ViewProduct");
        }
    }

}
