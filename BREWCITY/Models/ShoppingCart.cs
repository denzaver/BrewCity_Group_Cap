using BREWCITY.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
        public string ShoppingCartId { get; set; }  //need to set to int?
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

        public static ShoppingCart GetCart(IServiceProvider services) //
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>
                ()?.HttpContext.Session; //the '?' is a now check, performes the check before trying to access the session. dont have to create any if statements to check 

            var context = services.GetService<ApplicationDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString(); // '??' is a turnary operator(if statement) with a now check 
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Beer beer, int amount)
        {
            // here we retreive the shopping cart item or......
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Beer.BeerId == beer.BeerId && s.ShoppingCartId == ShoppingCartId); //we check to make sure that shopping cart Id's match

            if (shoppingCartItem == null) // .... if null, we create a new instance of ShoppingCartItem (not shopping cart)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Beer = beer,
                    Amount = amount
                    
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else //if the item is already in the cart, we increase the amount
            {
                shoppingCartItem.Amount++; //we dont need to add it to the DbContext since its already there, 
            }
            _context.SaveChanges();
        }

        //public int RemoveFromCart(Beer beer) // this is an int because we are decreasing the amount of a SINGLE item
        //    // first we need to make sure the beer actually exists
        //{

        }
    }

}
