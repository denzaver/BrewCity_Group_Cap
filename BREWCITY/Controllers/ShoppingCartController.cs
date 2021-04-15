using BREWCITY.Models;
using BREWCITY.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IBeerRepository _beerRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IBeerRepository beerRepository, ShoppingCart shoppingCart)
        {
            _beerRepository = beerRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index() //using this to return the cart total into our view
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems(); //retreives all items and sets the shoppingcartitems prop in shopping cart

            ShoppingCartIndexViewModel shoppingCartIndexViewModel = new ShoppingCartIndexViewModel //takes the total items and amount and puts them into this view model
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartIndexViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int beerId) //action that adds item to the shopping cart and redirects to the updates shopping cart
        {
            var selectedBeer = _beerRepository.GetAllBeer.FirstOrDefault(b => b.BeerId == beerId);

            if (selectedBeer != null)
            {
                _shoppingCart.AddToCart(selectedBeer, 1);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int beerId)
        {
            var selectedBeer = _beerRepository.GetAllBeer.FirstOrDefault(b => b.BeerId == beerId);

            if (selectedBeer != null)
            {
                _shoppingCart.RemoveFromCart(selectedBeer);
            }

            return RedirectToAction("Index");
        }


    }
}
