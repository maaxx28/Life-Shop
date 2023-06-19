using Microsoft.AspNetCore.Mvc;
using LifeShop.Models;
using LifeShop.Controllers;
namespace LifeShop.Controllers
{
    public class CheckOut : Controller
    {
        public IActionResult PaymentPage()
        {
            return View("PaymentPage");
        }
        public IActionResult NewOrder()
        {
            Payment thisPayment = new Payment(0);
            int loggedInCustomer = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;

            int cartID = Models.Customer.GetCart(loggedInCustomer);


            ShoppingCart thisCart = new(cartID);

            
            thisPayment.CustomerID = loggedInCustomer;
            thisPayment.PaymentAddress = HttpContext.Request.Form["addAddress"];
            thisPayment.PaymentCity = HttpContext.Request.Form["addCity"];
            thisPayment.PaymentState = HttpContext.Request.Form["addState"];
            thisPayment.PaymentZip = HttpContext.Request.Form["addZip"];
            thisPayment.CardNumber = HttpContext.Request.Form["addCardNumber"];
            thisPayment.CardType = HttpContext.Request.Form["addCardType"];
            thisPayment.CVC = Convert.ToInt32(HttpContext.Request.Form["addCVC"]);
            if(HttpContext.Request.Form["addAddress"] =="")
            {
                ViewData["Message"] = "No Billing Address is entered.";
                return View("PaymentPage");
            }
            else if (HttpContext.Request.Form["addCity"] == "")
            {
                ViewData["Message"] = "No Billing City is entered.";
                return View("PaymentPage");
            }
            else if (HttpContext.Request.Form["addState"] == "")
            {
                ViewData["Message"] = "No Billing State is entered.";
                return View("PaymentPage");
            }
            else if (HttpContext.Request.Form["addZip"] == "")
            {
                ViewData["Message"] = "No Billing Zipcode is entered.";
                return View("PaymentPage");
            }
            else if (HttpContext.Request.Form["addShipAddress"] == "")
            {
                ViewData["Message"] = "No Shiping Address is entered.";
                return View("PaymentPage");
            }
            else if (HttpContext.Request.Form["addShipCity"] == "")
            {
                ViewData["Message"] = "No Shipping City is entered.";
                return View("PaymentPage");
            }
            else if (HttpContext.Request.Form["addStateState"] == "")
            {
                ViewData["Message"] = "No Shipping State is entered.";
                return View("PaymentPage");
            }
            else if (HttpContext.Request.Form["addShipZip"] == "")
            {
                ViewData["Message"] = "No Shipping Zipcode is entered.";
                return View("PaymentPage");
            }
            else if (HttpContext.Request.Form["addCardNumber"] == "")
            {
                ViewData["Message"] = "No Card Number is entered.";
                return View("PaymentPage");
            }
            else if (HttpContext.Request.Form["addCardType"] == "")
            {
                ViewData["Message"] = "No Card Type is entered.";
                return View("PaymentPage");
            }
            else if (HttpContext.Request.Form["addCVC"] == "")
            {
                ViewData["Message"] = "No CVC is entered.";
                return View("PaymentPage");
            }
            else
            {
                ViewData["PaymentMessage"]= thisPayment.Save();

                
                //Reset Cart
                List<CartItem> deleteList = CartItem.GetListByCart(cartID);

                foreach (CartItem item in deleteList)
                {
                    item.Delete(item.ID);
                }


                //Generate Order
                Order thisOrder = new Order(0);
                thisOrder.TotalItems = thisCart.TotalItems;
                thisOrder.TotalCost = thisCart.TotalCost;
                thisOrder.PaymentID = thisPayment.ID;
                thisOrder.OrderDate = DateTime.Now;
                thisOrder.CustomerID = loggedInCustomer;
                //Saving Returns it's ID to find the Shipment
                int thisOrderID = thisOrder.Save();


                int shipID = thisOrder.GetShipment(thisOrderID);
                Shipping thisShipping = new Shipping(shipID);
                thisShipping.DeliveryDate = new DateTime(1900, 1,1);
                thisShipping.ShipAddress = HttpContext.Request.Form["addShipAddress"];
                thisShipping.ShipCity = HttpContext.Request.Form["addShipCity"];
                thisShipping.ShipState = HttpContext.Request.Form["addShipState"];
                thisShipping.ShipZip = Convert.ToInt32(HttpContext.Request.Form["addShipZip"]);
                thisShipping.Status = "Not Shipped";
                ViewData["ShippingMessage"] = thisShipping.Save();

                thisCart.TotalItems = 0;
                thisCart.TotalCost = 0;
                thisCart.Save();

                return View("SuccessPayment");

            }

        }
    }
}
