using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBank.Models
{
    public class RestReview
    {
        public int Id { get; set; }

        // public string Name { get; set; }
        [Range(1, 10)]
        [Required]
        public int Rating { get; set; }

        [StringLength(1024)]
        public string Body { get; set; }

        [Display(Name = "User Name")]
        [DisplayFormat(NullDisplayText = "anonymous")]
        public string ReviewerName { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
