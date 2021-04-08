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


        [ForeignKey("Beer")]​
        public string BeerId { get; set; }
        public virtual Beer Beer { get; set; }
        public IEnumerable<Beer> Beers { get; set; }


        [ForeignKey("Customer")]
        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
