using LifeShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace LifeShop.Controllers
{
    public class CustomerController : Controller
    {


        public IActionResult CustomerTools()
        {
            return View("CustomerTools");
        }
        public IActionResult CustomerDetails()
        {
            int loggedInCustomer = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;
            if (loggedInCustomer == 0)
            {
                return View("Logon");
            }
            else
            {
                ViewData["Edit"] = 0;
                ViewBag.CurrentCustomer = new Models.Customer(loggedInCustomer);

                return View("CustomerDetails");
            }

        }
        public IActionResult Edit()
        {
            int loggedInCustomer = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;
            ViewData["Edit"] = 1;
            ViewBag.CurrentCustomer = new Models.Customer(loggedInCustomer);
            return View("CustomerDetails");
        }
        public IActionResult OrderList()
        {
            int loggedInCustomer = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;

            if (loggedInCustomer == 0)
            {

            }
            else 
            {
                List<Order> orders = Models.Order.GetList();
                List<Order> myOrders = new List<Order>();
                foreach (Order myOrder in orders)
                {
                    if (myOrder.CustomerID == loggedInCustomer)
                    {
                        myOrders.Add(myOrder);
                    }
                }
                ViewBag.myOrders = myOrders;
            }
            return View("OrderList");
        }
        public IActionResult OrderDetails(int id) 
        {
            Order thisOrder = new Order(id);
            Payment thisPayment = new Payment(thisOrder.PaymentID);
            int shipID = thisOrder.GetShipment(id);
            Shipping thisShipment = new Shipping(shipID);
            ViewBag.ThisShipment = thisShipment;
            ViewBag.ThisOrder = thisOrder;
            ViewBag.ThisPayment = thisPayment;

            return View("OrderDetails");
        }
        public IActionResult CancelOrder(int id)
        {
            Shipping canceled = new Shipping(id);
            DateTime date = DateTime.Now;
            canceled.Status = "Order canceled by Customer on " + date.ToString("d");
            canceled.Save();
            OrderDetails(canceled.OrderID);
            return View("OrderDetails");
        }
        public ActionResult EditCustomer()
        {
            int thisCustomer = HttpContext.Session.GetInt32("_LoggedInCustomerID") ?? 0;
            Customer editCustomer = new Customer(thisCustomer);
            editCustomer.FirstName = HttpContext.Request.Form["editFirstName"];
            editCustomer.LastName = HttpContext.Request.Form["editLastName"];
            editCustomer.Address = HttpContext.Request.Form["editAddress"];
            editCustomer.City = HttpContext.Request.Form["editCity"];
            editCustomer.State = HttpContext.Request.Form["editState"];
            editCustomer.Zip = HttpContext.Request.Form["editZip"];
            editCustomer.Email = HttpContext.Request.Form["editEmail"];
            editCustomer.Phone = HttpContext.Request.Form["editPhone"];
            editCustomer.UserName = HttpContext.Request.Form["editUserName"];
            editCustomer.Password = HttpContext.Request.Form["editPassword"];

            string availableUserName = Models.Customer.ValidUserName(HttpContext.Request.Form["addUserName"].ToString());

            if (HttpContext.Request.Form["editUserName"].ToString() == "")
            {
                ViewData["NewCustomerMessage"] = "Username cannot be null.";
                return View("CustomerDetails");
            }
            else if (HttpContext.Request.Form["editUserName"].ToString() == availableUserName)
            {
                ViewData["NewCustomerMessage"] = "Username is not available";
                return View("CustomerDetails");
            }
            else if (HttpContext.Request.Form["editPassword"].ToString() == "")
            {
                ViewData["NewCustomerMessage"] = "Password must be filled out";
                return View("CustomerDetails");
            }
            else
            {
                editCustomer.Save();

                CustomerDetails();
                return View("CustomerDetails");
            }

        }
    }
}
