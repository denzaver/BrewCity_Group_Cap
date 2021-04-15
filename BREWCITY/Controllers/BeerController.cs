using BREWCITY.Models;
using BREWCITY.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Controllers
{
    public class BeerController : Controller 
    {
        private readonly IBeerRepository _beerRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BeerController(IBeerRepository beerRepository, ICategoryRepository categoryRepository)
        {
            _beerRepository = beerRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List()
        {
            var beerListViewModel = new BeerListViewModel();
            beerListViewModel.Beers = _beerRepository.GetAllBeer;
            /*beerListViewModel.CurrentCategory = _categoryRepository.GetAllCategories*/;
            return View(beerListViewModel);
        }
    }
}
