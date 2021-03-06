using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BREWCITY.Data;
using BREWCITY.Models;
using BREWCITY.Services;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BREWCITY.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGetLocalBreweriesService _getLocalBreweriesService;
        private readonly IGoogleMapService _googleMapService;

        public CustomersController(ApplicationDbContext context, IGetLocalBreweriesService getLocalBreweriesService, IGoogleMapService googleMapService)
        {
            _context = context;
            _getLocalBreweriesService = getLocalBreweriesService;
            _googleMapService = googleMapService;
        }

        // GET: Customers
        public IActionResult Index()
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.IdentityUserId == userId).FirstOrDefault();
            if(customer == null)
            {
                return View("Create");
            }

            var breweries = _context.Breweries.Where(x => x.ZipCode == customer.Zipcode);
            return View(breweries);
        }
        public async Task<IActionResult> GetList(Customer customer)
        {



            var deliveryDetails = _context.DeliveryDetailss.Where(d => d.CustomerId == customer.Id);
            if (customer == null)
            {
                return View("Create");
            }
            var details = _context.DeliveryDetailss.Where(d => d.CustomerId == customer.Id).LastOrDefault();
            if (details == null)
            {
                return View("CreateDeliveryDetails");//fill in later, create deliver details
            }



            var state = details.State;
            var city = details.City;
            var actionResult = await _getLocalBreweriesService.GetLocalBreweries(state);
            var filteredResult = actionResult.Where(b => b.City == city).ToList();

            //maps begin
            var listOfAddresses = new List<string>();

            foreach (var a in filteredResult)
            {
                var str = a.Street + a.City + a.State;
                var stringAddress = str.ToString();
                listOfAddresses.Add(stringAddress);

            }
            var count = filteredResult.Count() - 10;

            for (var i = count; i > 0; i--)
            {
                listOfAddresses.RemoveAt(10);
            }
            var data1 = _googleMapService.GetGoogleMap(listOfAddresses);
            var data = data1.ToString();
            var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            //var map = Convert.FromBase64String(base64Data);
            ViewBag.Map = base64Data;
            //maps end



            JsonBrewery[] actionResultArray = filteredResult.ToArray();
            return View(actionResultArray);
        }

        public IActionResult FilterBeers()
        {
            var types = _context.Beers.Select(x => x.Type).Distinct().ToList();
            types.Add("none");
            var breweryNames = _context.Breweries.Select(x => x.BusinessName).Distinct().ToList();
            breweryNames.Add("none");
            ViewBag.Type = new SelectList(types);
            ViewBag.Brewery = new SelectList(breweryNames);
            var beers = _context.Beers;
            return View(beers);
        }

        [HttpPost, ActionName("FilterBeers")]
        [ValidateAntiForgeryToken]
        public IActionResult FilterBeers(string type, string breweryName)
        {
            var types = _context.Beers.Select(x => x.Type).Distinct().ToList();
            var breweryNames = _context.Breweries.Select(x => x.BusinessName).Distinct().ToList();
            ViewBag.Type = new SelectList(types);
            types.Add("none");
            ViewBag.Brewery = new SelectList(breweryNames);
            breweryNames.Add("none");
            Brewery brewery = _context.Breweries.Where(x => x.BusinessName == breweryName).FirstOrDefault(); 
            List<Beer> beerList = null;

            if (type != "none" && breweryName != "none")
            {
                  
                 beerList = _context.Beers.Where(x => x.Type == type && x.BreweryId == brewery.BreweryId).ToList();
                return View(beerList);
            }
            else if(type != "none")
            {
                beerList = _context.Beers.Where(x => x.Type == type).ToList();
                return View(beerList);
            }
            else if(breweryName != "none")
            {
                beerList = _context.Beers.Where(x => x.BreweryId == brewery.BreweryId).ToList();
                return View(beerList);
            }
            else
            {
                return View(NotFound());
            }

        }

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beers = _context.Beers.Where(br => br.BreweryId == id);

            //var customer = await _context.Customers
            //    .Include(c => c.IdentityUser)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (beers == null)
            {
                return NotFound();
            }
            return View(beers);
        }

        //public async Task<IActionResult> GetLocalBreweries(string state)
        //{
        //    IActionResult actionResult = await _breweryService.GetBreweryList(state);
        //    return actionResult;
        //}

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BusinessName,BusinessRole,StreetAddress,Zipcode,Email,IdentityUserId")] Customer customer)
        {
            

            if (ModelState.IsValid)
            {
                customer.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        //Get
        public IActionResult CreateReview()
        {
            return View();

        }

        [HttpPost, ActionName("CreateReview")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview(int id, [Bind("Id,Text,BeerId,CustomerId")] Review review)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.IdentityUserId == userId).FirstOrDefault();
            review.CustomerId = customer.Id;
            review.BeerId = id;
            _context.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,BusinessName,BusinessRole,StreetAddress,Zipcode,Email,IdentityUserId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        public IActionResult MyReviews()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.IdentityUserId == userId).FirstOrDefault();
            var reviews = _context.Reviews.Where(x => x.CustomerId == customer.Id);
            return View(reviews);
        }

        //Get Review
        public IActionResult DeleteReview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var review = _context.Reviews.Where(x => x.ReviewId == id).FirstOrDefault();
            if(review == null)
            {
                return NotFound();
            }
            return View(review);
        }
        [HttpPost, ActionName("DeleteReview")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyReviews));
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        public async Task<IActionResult> EditReview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpPost, ActionName("EditReview")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReview(int id, [Bind("ReviewId, Text, BeerId, CustomerId")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(review.ReviewId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyReviews));
            }
            return View(nameof(MyReviews));
        }


        ////////// hey 
        // I'm gonna put some shopping cart stuff here

        public async Task<IActionResult> AddToCart(int? id)
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

        [HttpPost, ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int beerId, int amount)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.IdentityUserId == userId).FirstOrDefault();
            var beer = _context.Beers.Where(br => br.BeerId == beerId).FirstOrDefault();
            TempCart cart = new TempCart();
            cart.Amount = amount;
            cart.BeerId = beerId;
            cart.Customer = customer;
            cart.CustomerId = customer.Id;
            cart.Beer = beer;
            //_context.
            //_context.SaveChanges();

            return View();
        }



    }
}
