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
        public int Stock { get; set; }
        public int Price { get; set; }


        // ************************  NEED TO CREATE WHEN OTHER MODELS/VIEWS ARE SET UP AND RUNNIG ************************

        //[ForeignKey("BeerReview")]

        //public IEnumerable<BeerReview> BeerReviews { get; set; }
        [ForeignKey("Brewery")]
        public int BreweryId { get; set; }
        //fk beer
    }
}
