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
    }
}
