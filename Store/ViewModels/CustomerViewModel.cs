using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Models;

namespace Store.ViewModels
{
    public class CustomerViewModel
    {
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public Order Order { get; set; }

    }
}