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
        public DbSet <Review> Reviews { get; set; }
        public DbSet <Sale> Sales { get; set; }
    }
}
