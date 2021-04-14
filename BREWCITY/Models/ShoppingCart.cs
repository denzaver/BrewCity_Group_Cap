using BREWCITY.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace BREWCITY.Models
{
    public class ShoppingCart
    {
        // the shopping cart is going to work by checking if the user already has a session & an active shopping cart
        //
        private readonly ApplicationDbContext _context;

        [Key]
        public int ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Beer")]
        public int BeerId { get; set; }
        public Beer Beer { get; set; }


        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }
    }

}
