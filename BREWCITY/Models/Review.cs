using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Text { get; set; }

        [ForeignKey("Beer")]
        public int BeerId { get; set; }
        public Beer Beer { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
