using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DelpinWebApi.Models
{
    public class Order
    {

        [Key]
        public int OrderId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("Ressource")]
        public int RessourceId { get; set; }
        public Ressource Ressource { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
