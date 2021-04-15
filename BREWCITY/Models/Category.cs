using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class Category
    {
        public int CatagoryId { get; set; }
        public string CatagoryName { get; set; }
        public string CatagoryDescription { get; set; }
        public List<Beer> Beers { get; set; }
    }
}
