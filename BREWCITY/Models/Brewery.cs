﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class Brewery
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        public int ZipCode { get; set; }

        public List<Beer> BeerList { get; set; }

        public string Email { get; set; }


        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }

    }
}
