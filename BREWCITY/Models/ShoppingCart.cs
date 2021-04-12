using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace BREWCITY.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Beer")]
        public int BeerId { get; set; }
        public Beer Beer { get; set; }

    }
}
