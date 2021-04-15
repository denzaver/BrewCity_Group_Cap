using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public interface IBeerRepository
    {
        IEnumerable<Beer> GetAllBear { get; }
        IEnumerable<Beer> GetBeerOnSale { get; }
        Beer GetBeerById(int beerId);
    }
}
