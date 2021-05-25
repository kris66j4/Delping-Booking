using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DelpinWebApi.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
    }
}
