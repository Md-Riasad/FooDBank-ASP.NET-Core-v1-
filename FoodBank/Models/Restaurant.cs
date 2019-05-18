using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBank.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual List<RestReview> Reviews { get; set; }
    }
}
