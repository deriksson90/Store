using Store.Models;
using Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Store.Controllers
{
    public class CheckOutController : Controller
    {
        conDb storeDB = new conDb();


        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            
            TryUpdateModel(order);
            

            storeDB.Orders.Add(order);
            storeDB.SaveChanges();

            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);


                 return RedirectToAction("Complete",
                  new { id = order.OrderId });

        }

        public ActionResult Complete(int id)
        {

            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {

                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
                CartWeight = cart.GetWeight(),
                Fragility = cart.GetFragileCost(),
                Order = cart.GetOrder(id),
                OrderDetails = cart.GetOrderId(id),

            };

            return View(viewModel);
        }

        public ActionResult End()
        {
            return View();
        }


    }
    }

