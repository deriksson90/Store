using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Models
{
    public class ShoppingCart
    {
        conDb storeDB = new conDb();
        private int extraCostWeight = 500;

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        //Hämtar shoppingCart
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // 
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        //Kostnad baserad på färdlängd
        public int TravlingCost()
        {
            int distance = 0;
            int uFifty = 80;
            int oFifty = 320;
            int oHund = 720;
            int travlingCost = 0;
            
            if(distance < 50)
            {
               travlingCost = uFifty + (5 * distance);
            }
            if(distance > 50 && distance < 100)
                {
                travlingCost = oFifty + (4 * distance);
                }
            if(distance > 100)
            {
                travlingCost = oHund + (3 * distance);
            }

            return travlingCost;
        }

        public int? GetFragileCost()
        {
            return (from cartItems in storeDB.Carts
             where cartItems.CartId == ShoppingCartId
             select (int?)cartItems.Count *
             cartItems.Item.Fragile).Sum();
        }

        public string GetTransport()
        {
            int fragile = 0;
            String fragileGoods = "";

            var transport = storeDB.Carts.ToList();
            
            foreach(var item in transport)
            {
                fragile = item.Item.Fragile;
            };

            if(fragile == 150 || fragile == 750)
            {
                fragileGoods = "Ja";
            }
            else
            {
                fragileGoods = "Nej";
            }
            return fragileGoods;

        }

        public void AddToCart(Item item)
        {

            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ItemId == item.ItemId);

            if (cartItem == null)
            {

                cartItem = new Cart
                {
                    ItemId = item.ItemId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {

                cartItem.Count++;
            }

            storeDB.SaveChanges();
        }

        internal List<OrderDetail> GetOrderId(int id)
        {
            return storeDB.OrderDetails.ToList();

        }

        public int RemoveFromCart(int id)
        {
            // Get the cart 
            var cartItem = storeDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                //--if (cartItem.Count > 1)
                //--{
                //--    cartItem.Count--;
                //--    itemCount = cartItem.Count;
                //--}
                //--else
                //--{
                storeDB.Carts.Remove(cartItem);
                //--}
                // Save changes 
                storeDB.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }

            storeDB.SaveChanges();
        }


        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public Order GetOrder(int id)
        {
            return storeDB.Orders.Single(o => o.OrderId == id);
        }

        public int GetCount()
        {

            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {

            decimal? totalCost = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Item.Price).Sum();

            decimal weight = GetWeight();
            decimal volume = GetVolume();
            int? fragExtra = GetFragileCost();

            totalCost += fragExtra;

            if(weight >= 100 || volume > 1)
            {
                totalCost += extraCostWeight;           
               
            }
            
            return totalCost ?? decimal.Zero;
      
        }

        public string TruckNeeded()
        {
            string loadingGoods = "";
            decimal weight = GetWeight();
            decimal volume = GetVolume();

            if (weight >= 100 || volume > 1)
            {
                loadingGoods = "Lastbil: 500kr";
            }

            return loadingGoods;

        }


        public  decimal GetWeight()
        {
            decimal? total_w = (from cartItems in storeDB.Carts
                                where cartItems.CartId == ShoppingCartId
                                select (int?)cartItems.Count *
                                cartItems.Item.Weight).Sum();

            return total_w ?? decimal.Zero;
        }

        public decimal GetVolume()
        {
            decimal? total_v = (from cartItems in storeDB.Carts
                                where cartItems.CartId == ShoppingCartId
                                select (int?)cartItems.Count *
                                cartItems.Item.Volume).Sum();


            return total_v ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ItemId = item.ItemId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Item.Price,
                    Quantity = item.Count,
                    TotalWeight = item.Item.Weight,
                    TotalVolume = item.Item.Volume

                };

                orderTotal += (item.Count * item.Item.Price);

                storeDB.OrderDetails.Add(orderDetail);

            }

            order.Total = orderTotal;
        //   EmptyCart();

            storeDB.SaveChanges();

            return order.OrderId;
        }
        
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {

                    Guid tempCartId = Guid.NewGuid();

                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
    }
}
