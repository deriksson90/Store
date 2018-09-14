using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Models
{
    [Bind(Exclude = "OrderId")]
    public partial class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Förnamn krävs")]
        [DisplayName("Förnamn")]
        [StringLength(160)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Efternamn krävs")]
        [DisplayName("Efternamn")]
        [StringLength(160)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Adress krävs")]
        [StringLength(70)]
        [DisplayName("Adress")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Stad krävs")]
        [StringLength(40)]
        [DisplayName("Stad")]
        public string City { get; set; }
        [Required(ErrorMessage = "Län krävs")]
        [DisplayName("Län")]
        [StringLength(40)]
        public string State { get; set; }
        [Required(ErrorMessage = "Postnummer krävs")]
        [DisplayName("Postnummer")]
        [StringLength(10)]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Land krävs")]
        [StringLength(40)]
        [DisplayName("Land")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Telefonnummer krävs")]
        [StringLength(24)]
        [DisplayName("Telefonnummer")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email krävs")]
        [DisplayName("Email Adress")]

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
