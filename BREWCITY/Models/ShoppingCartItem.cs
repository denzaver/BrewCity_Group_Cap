using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public string ShoppingCartItemId { get; set; }
        public string ShoppingCartId {get; set;}

        [ForeignKey("Beer")]
        public Beer Beer { get; set; }
        public int Amount { get; set; }
    }
}
