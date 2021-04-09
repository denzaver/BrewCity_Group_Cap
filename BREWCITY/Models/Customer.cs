﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessRole { get; set; }
        public string Bio { get; set; }
        public string StreetAddress { get; set; }
        public int Zip { get; set; }
        public string Email { get; set; }


        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }


        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
