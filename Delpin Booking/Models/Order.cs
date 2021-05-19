using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Delpin_Booking.Models
{
    public class Order
    {
        [Key]
        public int Order_id { get; set; }

        [ForeignKey("Customer")]
        public Customer Customer { get; set; }
        public int Customer_id { get; set; }
        [ForeignKey("Ressource")]
        public Ressource Ressource { get; set; }
        public int Ressource_id { get; set; }
        public DateTime date { get; set; }
        public decimal Price { get; set; }
    }
}
