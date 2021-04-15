using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class Beer
    {
        [Key]
        public int BeerId { get; set; }
        public string BeerName { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public bool OnSale { get; set; }
        public bool InStock { get; set; }

        //[ForeignKey("Category")]
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }

        [ForeignKey("Brewery")]
        public int BreweryId { get; set; }

    }
}
