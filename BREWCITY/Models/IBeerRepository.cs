using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public interface IBeerRepository
    {
        IEnumerable<Beer> GetAllBeer { get; }
        IEnumerable<Beer> GetBeerOnSale { get; }
        Beer GetBeerById(int beerId);
    }
}
