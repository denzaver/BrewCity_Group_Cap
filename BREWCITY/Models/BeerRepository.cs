﻿using BREWCITY.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class BeerRepository : IBeerRepository
    {
        private readonly ApplicationDbContext _context;

        public BeerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Beer> GetAllBear
        {
            get
            {
                return _context.Beers.Include(c => c.Category);
            }
        }

        public IEnumerable<Beer> GetBeerOnSale
        {
            get
            {
                return _context.Beers.Include(c => c.Category).Where(b => b.OnSale);
            }
        }

        public Beer GetBeerById(int beerId)
        {
            return _context.Beers.FirstOrDefault(b => b.BeerId == beerId);
        }
    }
}
