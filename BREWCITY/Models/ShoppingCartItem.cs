﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class ShoppingCartItem
    {
        
        public string ShoppingCartItemId { get; set; }
        public string ShoppingCartId {get; set;}
        public Beer Beer { get; set; }
        public int Amount { get; set; }
    }
}