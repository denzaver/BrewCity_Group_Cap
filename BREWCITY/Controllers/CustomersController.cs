﻿using System;
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

namespace BREWCITY.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly BreweryService _breweryService;

        public CustomersController(ApplicationDbContext context) //, BreweryService breweryService)
        {
            _context = context;
            //_breweryService = breweryService;
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
        
        public IActionResult SeeReviews(int id)
        {
            var reviews = _context.Reviews.Where(x => x.BeerId == id);
            return View(reviews);
        }

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
                
                TempCart cart = new TempCart();
                cart.Customer = customer;
                cart.CustomerId = customer.Id;
                cart.Beers = null;
                cart.Amount = 0;
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


        [HttpPost, ActionName("SeeSales")]
        [ValidateAntiForgeryToken]
        public IActionResult SeeSales()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.IdentityUserId == userId).FirstOrDefault();
            List<Sale> sales = null;
            
            
                sales = _context.Sales.Where(x => x.Zip == customer.Zipcode).ToList();

            return View(sales);
        }

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
        public IActionResult AddToCart(int beerId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.IdentityUserId == userId).FirstOrDefault();
            var cart = new ShoppingCart();
            cart.Id = customer.Id;
            cart.Customer = customer;
            var beer = _context.Beers.Where(br => br.BeerId == beerId).FirstOrDefault();
            cart.Beer = beer;
            cart.BeerId = beer.BeerId;
            cart.Quantity = 1;
            beer.Stock -= 1;
            Sale sale = new Sale();
            sale.Beer = beer;
            sale.BeerId = beerId;
            sale.quantity = 1;
            sale.Zip = customer.Zipcode;

            //var cart = _context.ShoppingCarts.Where(x => x.CustomerId == customer.Id).FirstOrDefault();
            //Beer beer = _context.Beers.Where(x => x.BeerId == id).FirstOrDefault();
            //cart.CustomerId = customer.Id;
            //cart.Quantity += beer.Price;
            //cart.Customer = customer;
            //cart.TempCartId = cart.TempCartId;
            //cart.Beers.Add(beer);
            //beer.Stock -= 1;
            _context.Add(cart);
            _context.Update(beer);
            _context.SaveChangesAsync();

            //_context.
            //_context.SaveChanges();

            return View();
        }
        //Get
        public IActionResult RemoveFromCart(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var beer = _context.Beers.Where(x => x.BeerId == id).FirstOrDefault();
            return View(beer);
        }

        [HttpPost, ActionName("RemoveFromCart")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int id)
        {
            var beer = _context.Beers.Where(x => x.BeerId == id).FirstOrDefault();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.IdentityUserId == userId).FirstOrDefault();
            var cartToRemoveByBeer = _context.ShoppingCarts.Where(ct => ct.BeerId == beer.BeerId);
            var cartToRemove = cartToRemoveByBeer.Where(ct => ct.CustomerId == customer.Id);
            //var cart = _context.TempCarts.Where(ct => ct.CustomerId == customer.Id).FirstOrDefault();
            //cart.Beers.Remove(beer);
            beer.Stock += 1;
            //cart.Amount -= beer.Price;
            //cart.CustomerId = customer.Id;
            //cart.Customer = customer;
            _context.Remove(cartToRemove);
            _context.SaveChanges();
            return View();
        }
        [HttpPost, ActionName("ViewCart")]
        [ValidateAntiForgeryToken]
        public IActionResult ViewCart()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.IdentityUserId == userId).FirstOrDefault();
            var beers = _context.ShoppingCarts.Where(ct => ct.CustomerId == customer.Id).Select(ct => ct.Beer);
            return View(beers);
        }
        //Get
        public IActionResult ClearCart(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var cart = _context.ShoppingCarts.Where(x => x.CustomerId == id).FirstOrDefault();
            return View(cart);
        }

        [HttpPost, ActionName("ClearCart")]
        [ValidateAntiForgeryToken]
        public IActionResult ClearCart(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.IdentityUserId == userId).FirstOrDefault();
            var carts = _context.ShoppingCarts.Where(ct => ct.CustomerId == id);
            foreach(ShoppingCart cart in carts)
            {
                cart.Beer.Stock += 1;
                _context.Update(cart.Beer);
                _context.Remove(cart);
                _context.SaveChanges();
            }
            //carts.TempCartId = cart.TempCartId;
            //carts.CustomerId = customer.Id;
            //carts.Customer = customer;
            //carts.Amount = 0;
            //carts.Beers = null;
            
            return View();
        }
       


    }
}
