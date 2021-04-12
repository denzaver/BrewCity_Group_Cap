using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class ViewModel
    {
        public Brewery Brewery {get; set;}
        public List<Review> Reviews { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
