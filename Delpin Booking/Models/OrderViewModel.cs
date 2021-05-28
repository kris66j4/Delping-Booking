using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delpin_Booking.Models
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public SelectList CustomerIds { get; set; }
        public int CustomerId { get; set; }
        public SelectList RessourceIds { get; set; }
        public int RessourceId { get; set; }
        public string SearchString { get; set; }
    }
}
