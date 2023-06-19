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
        public IActionResult AddToCart(int id)
        {
            int loggedInCustomer = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;
            

            int custCart = Models.Customer.GetCart(loggedInCustomer);
            ShoppingCart thisCart =  new(custCart);
            Product thisProduct = new Product(id);

            CartItem newItem = new CartItem(0);
            
            newItem.ProductID = id;
            newItem.CartID = thisCart.ID;
            newItem.Quantity = 1;
            newItem.Price = (thisProduct.Price * (100 - thisProduct.Discount)) / 100;


            
            newItem.Save();

            List<CartItem> cartItems = CartItem.GetListByCart(custCart);
            int cartQuantity = 0;
            decimal cartCost = 0;
            foreach (CartItem item in cartItems)
            {
                cartQuantity += item.Quantity;
                cartCost += item.Price;
            }
            thisCart.TotalItems = cartQuantity;
            thisCart.TotalCost = cartCost;
            String a = thisCart.Save();

            ShoppingIndex();
            ViewData["Message"]= a;
            return View("Shop");
        }
        public IActionResult ViewCart() 
        {
            int loggedInCustomer = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;
            int custCart = Models.Customer.GetCart(loggedInCustomer);
            ShoppingCart thisCart = new ShoppingCart(custCart);
            List<CartItem> CartItems = Models.CartItem.GetListByCart(thisCart.ID);
            ViewBag.ShoppingCart = CartItems;
            ViewBag.CartDetails = thisCart;
            
            return View("ViewCart");
        }
        public IActionResult RemoveItem(int id)
        {
            CartItem thisItem = new CartItem(id);
            ViewData["Message"]= thisItem.Delete(id);

            int loggedInCustomer = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;
            int custCart = Models.Customer.GetCart(loggedInCustomer);
            ShoppingCart thisCart = new(custCart);

            List<CartItem> cartItems = CartItem.GetListByCart(custCart);
            int cartQuantity = 0;
            decimal cartCost = 0;
            foreach (CartItem item in cartItems)
            {
                cartQuantity += item.Quantity;
                cartCost += item.Price;
            }
            thisCart.TotalItems = cartQuantity;
            thisCart.TotalCost = cartCost;
            thisCart.Save();
            
            ViewCart();
            return View("ViewCart");
        }
        public IActionResult EmptyCart()
        {
            int loggedInCustomer = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;
            int cartID = Models.Customer.GetCart(loggedInCustomer);
            List<CartItem> deleteList = CartItem.GetListByCart(cartID);

            foreach(CartItem item in deleteList) 
            {
                item.Delete(item.ID);
            }

            ShoppingCart thisCart = new(cartID);
            thisCart.TotalItems = 0;
            thisCart.TotalCost = 0;

            ViewCart();
            return View("ViewCart");
        }
    }

}
