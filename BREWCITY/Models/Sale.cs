using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public int quantity { get; set; }

        [ForeignKey("Beer")]
        public int beerId { get; set; }
        public  Beer BeerName { get; set;}

    }
}
