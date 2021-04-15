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

namespace BREWCITY.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGetLocalBreweriesService _getLocalBreweriesService;

        public CustomersController(ApplicationDbContext context, IGetLocalBreweriesService getLocalBreweriesService) 
        {
            _context = context;
            _getLocalBreweriesService = getLocalBreweriesService;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Customers.Include(c => c.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
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

        public async Task<IActionResult> GetList(Customer customer)
        {

            var deliveryDetails = _context.DeliveryDetailss.Where(d => d.CustomerId == customer.Id);
            if(customer == null)
            {
                return View("Create");
            }
            var details = _context.DeliveryDetailss.Where(d => d.CustomerId == customer.Id).LastOrDefault();
            if(details == null)
            {
                return View("CreateDeliveryDetails");//fill in later, create deliver details
            }
            
            var state = details.State;
            var city = details.City;
            var actionResult = await _getLocalBreweriesService.GetLocalBreweries(state);
            var filteredResult = actionResult.Where(b => b.City == city).ToList();
            JsonBrewery[] actionResultArray = filteredResult.ToArray();
            return View(actionResultArray);
        }
        // GET: DeliveryDetails/Create
        public IActionResult CreateDeliveryDetails()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: DeliveryDetails/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDeliveryDetails([Bind("Id,CustomerId,ResponsiblePartyName,PhoneNumber,Street,City,State,StateAlcoholLicenceNumber")] DeliveryDetails deliveryDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", deliveryDetails.CustomerId);
            return View(deliveryDetails);
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
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview([Bind("Id,Text,BeerId,CustomerId")] Review review)
        {
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

        private bool UnsecuredCreditCardInformationExists(int id)
        {
            return _context.UnsecuredCreditCardInformations.Any(e => e.Id == id);
        }
        // GET: UnsecuredCreditCardInformations
        public async Task<IActionResult> PaymentMethods()
        {
            var applicationDbContext = _context.UnsecuredCreditCardInformations.Include(u => u.Customer);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> PaymentMethodDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unsecuredCreditCardInformation = await _context.UnsecuredCreditCardInformations
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unsecuredCreditCardInformation == null)
            {
                return NotFound();
            }

            return View(unsecuredCreditCardInformation);
        }

        // GET: UnsecuredCreditCardInformations/Create
        public IActionResult CreatePaymentMethod()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePaymentMethod([Bind("Id,CustomerId,CompanyNameOnCreditCardRequired,CreditCardNumber,Month,Year,CVC")] UnsecuredCreditCardInformation unsecuredCreditCardInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unsecuredCreditCardInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PaymentMethods));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", unsecuredCreditCardInformation.CustomerId);
            return View(unsecuredCreditCardInformation);
        }

        // GET: UnsecuredCreditCardInformations/Edit/5
        public async Task<IActionResult> EditPaymentMethod(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unsecuredCreditCardInformation = await _context.UnsecuredCreditCardInformations.FindAsync(id);
            if (unsecuredCreditCardInformation == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", unsecuredCreditCardInformation.CustomerId);
            return View(unsecuredCreditCardInformation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPaymentMethod(int id, [Bind("Id,CustomerId,CompanyNameOnCreditCardRequired,CreditCardNumber,Month,Year,CVC")] UnsecuredCreditCardInformation unsecuredCreditCardInformation)
        {
            if (id != unsecuredCreditCardInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unsecuredCreditCardInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnsecuredCreditCardInformationExists(unsecuredCreditCardInformation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PaymentMethods));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", unsecuredCreditCardInformation.CustomerId);
            return View(unsecuredCreditCardInformation);
        }


        public async Task<IActionResult> DeletePaymentMethod(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unsecuredCreditCardInformation = await _context.UnsecuredCreditCardInformations
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unsecuredCreditCardInformation == null)
            {
                return NotFound();
            }

            return View(unsecuredCreditCardInformation);
        }

        // POST: UnsecuredCreditCardInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePaymnentMethodConfirmed(int id)
        {
            var unsecuredCreditCardInformation = await _context.UnsecuredCreditCardInformations.FindAsync(id);
            _context.UnsecuredCreditCardInformations.Remove(unsecuredCreditCardInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PaymentMethods));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
