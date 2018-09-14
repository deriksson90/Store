using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;



namespace Store.Models
{
    [Bind(Exclude = "ItemId")]
    public class Item
    {
        [ScaffoldColumn(false)]
        public int ItemId { get; set; }

        [DisplayName("Kategori")]
        public int CategoryId { get; set; }

        [DisplayName("Producer")]
        public int ProducerId { get; set; }

        [Required(ErrorMessage = "Välj titel")]
        [StringLength(160)]

        public string Title { get; set; }

        [Required(ErrorMessage = "Välj pris")]
        [Range(0.1, 1000000, ErrorMessage = "Pris måste vara mellan 0.1 och 100000")]

        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }

        public int Fragile { get; set; }

        public bool InstHelp { get; set; }


        public virtual Category Category { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
        public virtual Order Order { get; set; }


    }
}