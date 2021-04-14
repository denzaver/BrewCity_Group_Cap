using BREWCITY.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index() //using this to return the cart total into our view
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems(); //retreives all items and sets the shoppingcartitems prop in shopping cart

            var shoppingCartViewModel = new ShoppingCartViewModel //takes the total items and amount and puts them into this view model
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartItems()
            };
            return View(shoppingCartViewModel);
        }
    }
}
