using BREWCITY.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class BeerRepository : IBeerRepository
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<Beer> GetAllBear => throw new NotImplementedException();

        public IEnumerable<Beer> GetBeerOnSale => throw new NotImplementedException();

        public Beer GetBeerById(int beerId)
        {
            throw new NotImplementedException();
        }
    }
}
