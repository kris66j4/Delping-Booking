using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Delpin_Booking.Models
{
    public class Ressource
    {
        [Key]
        public int Ressource_id { get; set; }

        [ForeignKey("Department")]
        public Department Department { get; set; }
        public int Department_id { get; set; }
       
       
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
      
    }
}
