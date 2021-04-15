using BREWCITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.ViewModels
{
    public class BeerListViewModel
    {
        public IEnumerable<Beer> Beers { get; set; }
        public string CurrentCategory { get; set; }
    }
}
