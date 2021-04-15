using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BREWCITY.Data;
using BREWCITY.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BREWCITY.Controllers
{
    [Authorize(Roles = "Brewery")]
    public class BreweriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BreweriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Breweries
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brewery = _context.Breweries.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if(brewery == null)
            {
                return NotFound();
            }
            var beers = _context.Beers.Where(br => br.BreweryId == brewery.BreweryId);
            return View(beers);

            //var applicationDbContext = _context.Breweries.Include(b => b.IdentityUser);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Breweries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }




            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brewery = _context.Breweries.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if (brewery == null)
            {
                return NotFound();
            }
            //Once Beers hav FK pointing to Brewery...
            //Query Beer table to find all beers with the ID of this brewery
            //Query Review table to find all Reviews for the Beers we just found
            List<Beer> Beers = _context.Beers.Where(x => x.BreweryId == brewery.BreweryId).ToList();
            List<Review> reviews = null;
            List<Sale> sales = null;
            ViewModel viewModel = new ViewModel();
            for (int i = 0; i < Beers.Count(); i++)
            {
               reviews = _context.Reviews.Where(x => x.BeerId == Beers[i].BeerId).ToList();
               sales = _context.Sales.Where(x => x.BeerId == Beers[i].BeerId).ToList();
            
            }
            
            
            viewModel.Reviews = reviews;
            viewModel.Sales = sales;
            
            
            

            return View(viewModel);
        }

        // GET: Breweries/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Breweries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BreweryId,FirstName,LastName,BusinessName,ZipCode,Email,IdentityUserId")] Brewery brewery)
        {
            if (ModelState.IsValid)
            {
                brewery.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(brewery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", brewery.IdentityUserId);
            return View("Index");
        }
        public IActionResult AddBeer()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        [HttpPost, ActionName("AddBeer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBeer([Bind("Id,BeerName,Type,Stock,Price")]Beer beer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brewery = _context.Breweries.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            beer.BreweryId = brewery.BreweryId;
            if (ModelState.IsValid)
            {
                _context.Add(beer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        // GET: Breweries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Breweries.FindAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", brewery.IdentityUserId);
            return View(brewery);
        }

        // POST: Breweries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,BusinessName,ZipCode,Email,IdentityUserId")] Brewery brewery)
        {
            if (id != brewery.BreweryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brewery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreweryExists(brewery.BreweryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", brewery.IdentityUserId);
            return View(brewery);
        }

        public async Task<IActionResult> UpdateBeer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            return View(beer);
        }

        [HttpPost, ActionName("UpdateBeer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBeer([Bind("BeerId,BeerName,Type,Stock,Price")] Beer beer)
        {
            // if (id != beer.BeerId)
            //{
            //  return NotFound();
            //}
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brewery = _context.Breweries.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            beer.BreweryId = brewery.BreweryId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerExists(beer.BeerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View("Index");
        }

        // GET: Breweries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Breweries
                .Include(b => b.IdentityUser)
                .FirstOrDefaultAsync(m => m.BreweryId == id);
            if (brewery == null)
            {
                return NotFound();
            }

            return View(brewery);
        }

        // POST: Breweries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brewery = await _context.Breweries.FindAsync(id);
            _context.Breweries.Remove(brewery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteBeer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = _context.Beers.Where(m => m.BeerId == id).FirstOrDefault();
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        [HttpPost, ActionName("DeleteBeer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreweryExists(int id)
        {
            return _context.Breweries.Any(e => e.BreweryId == id);
        }

        private bool BeerExists(int id)
        {
            return _context.Beers.Any(e => e.BeerId == id);
        }
    }
}
