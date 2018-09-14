using Store.Models;
using Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
    
{
    
    public class ShoppingCartController : Controller
    {
        conDb storeDB = new conDb();

        public ActionResult Index()
        {
       
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {

                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
                CartWeight = cart.GetWeight(),
                Fragility = cart.GetFragileCost(),
                GetTransport = cart.GetTransport(),
                TruckNeeded = cart.TruckNeeded(),

            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {

            var addedItem = storeDB.Items
                .Single(item => item.ItemId == id);
            

            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedItem);

           
           decimal weight = cart.GetWeight();
           decimal volume = cart.GetVolume();

            if(weight > 1000 || volume > 10)
            {
                return RedirectToAction("CustomerSupport");
            }

            return RedirectToAction("Index");
        }

        public ActionResult CustomerSupport()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartWeight = cart.GetWeight(),
                CartVolume = cart.GetVolume()

            };

            return View(viewModel);
        }


        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string itemName = storeDB.Carts
                .Single(item => item.ItemId == id).Item.Title;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
             
            return View();
        }
       
    }
}