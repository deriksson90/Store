using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Models;

namespace Store.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Order Order { get; set; }
        public List<Item> Items { get; set; }
        public decimal CartTotal { get; set; }
        public decimal CartWeight { get; set; }
        public decimal CartVolume { get; set; }
        public int? Fragility { get; set; }
        public string GetTransport { get; set; }
        public string TruckNeeded { get; set; }
        public int Distance { get; set; }

    }
}