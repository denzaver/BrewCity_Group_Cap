using BREWCITY.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BREWCITY.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins {get; set;}
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Review> Reviews { get; set; }
  

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Id = "2977ce1c-9089-4508-be3d-02e1cda9e07c",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "351ab8f6-41be-426b-9974-3bc2f5a75bff"

                },
                  new IdentityRole
                  {
                      Id = "7e82de51-7342-41f0-bffa-ff154800a55b",
                      Name = "Customer",
                      NormalizedName = "CUSTOMER",
                      ConcurrencyStamp = "7bb9037c-277b-485d-a150-e6a8006a20eb"
                  },
                 new IdentityRole
                 {
                     Id = "d7e5c8ea-cbc2-4d66-81cf-efae5fefd137",
                     Name = "Brewery",
                     NormalizedName = "BREWERY",
                     ConcurrencyStamp = "f97e27aa-a70d-4603-b296-a3f6859dd253"
                 });

            builder.Entity<Brewery>().HasData(
                new Brewery
                {
                    BreweryId = 1,
                    FirstName = "Brian",
                    LastName = "Hayes",
                    BusinessName = "Manhattan Project",
                    ZipCode = "75087",
                    Email = "brian@Hotmail.com",
                    IdentityUserId = "f97e27aa-a70d-4603-b296-a3f6859dd253"
                },
                new Brewery
                {
                    BreweryId = 2,
                    FirstName = "Kate",
                    LastName = "Reed",
                    BusinessName = "New Belgium",
                    ZipCode = "75087",
                    Email = "kate@icloud.com",
                    IdentityUserId = "f97e27aa-a70d-4603-b296-a3f6859dd253"
                },
                new Brewery
                {
                    BreweryId = 3,
                    FirstName = "Rosco",
                    LastName = "Ocsor",
                    BusinessName = "Altstadt",
                    ZipCode = "75087",
                    Email = "Rosco@gmail",
                    IdentityUserId = "f97e27aa-a70d-4603-b296-a3f6859dd253"
                }
                );
            builder.Entity<Beer>().HasData(
                new Beer
                {
                    BeerId = 1,
                    BeerName = "Half Life",
                    Type = "IPA",
                    Stock = 10,
                    Price = 100,
                    BreweryId = 1
                },
                new Beer
                {
                    BeerId = 2,
                    BeerName = "Plutonium",
                    Type = "Porter",
                    Stock = 10,
                    Price = 100,
                    BreweryId = 1
                },
                new Beer
                {
                    BeerId = 3,
                    BeerName = "Necessary Evil",
                    Type = "Lager",
                    Stock = 10,
                    Price = 100,
                    BreweryId = 1
                },
                new Beer
                {
                    BeerId = 4,
                    BeerName = "Voodoo Ranger",
                    Type = "IPA",
                    Stock = 10,
                    Price = 100,
                    BreweryId = 2
                },
                new Beer
                {
                    BeerId = 5,
                    BeerName = "Fat Tire",
                    Type = "Amber",
                    Stock = 10,
                    Price = 100,
                    BreweryId = 2
                },
                new Beer
                {
                    BeerId = 6,
                    BeerName = "Accumulation",
                    Type = "Weisse",
                    Stock = 10,
                    Price = 100,
                    BreweryId = 2
                },
                new Beer
                {
                    BeerId = 7,
                    BeerName = "Hefeweizen",
                    Type = "Weisse",
                    Stock = 10,
                    Price = 100,
                    BreweryId = 3
                },
                new Beer
                {
                    BeerId = 8,
                    BeerName = "Amber",
                    Type = "Amber",
                    Stock = 10,
                    Price = 100,
                    BreweryId = 3
                },
                new Beer
                {
                    BeerId = 9,
                    BeerName = "Lager",
                    Type = "Lager",
                    Stock = 10,
                    Price = 100,
                    BreweryId = 3
                }
                );
            base.OnModelCreating(builder);
            builder.Entity<Customer>()
            .HasData(
            new Customer
            {
                Id = 1,
                FirstName = "Thomas",
                LastName = "Thatch",
                BusinessName = "Knights and Roses",
                BusinessRole = "COO",
                StreetAddress = "123 nw 4th st",
                Zipcode = "75087",
                Email = "ThomasT@gmail.com"
            },
            new Customer
            {
                Id = 2,
                FirstName = "Gregory",
                LastName = "Thatch",
                BusinessName = "Roses and Knights",
                BusinessRole = "COO",
                StreetAddress = "126 nw 4th st",
                Zipcode = "75087",
                Email = "GregT@gmail.com"
            },
            new Customer
            {
                Id = 3,
                FirstName = "Adam",
                LastName = "Richmond",
                BusinessName = "Vally Coast Diner",
                BusinessRole = "General Manager",
                StreetAddress = "526 se Grapes st",
                Zipcode = "75087",
                Email = "AdamR@gmail.com"
            }
            );
            base.OnModelCreating(builder);
            builder.Entity<Review>()
            .HasData(
                new Review
                {
                    Id = 1,
                    Text = "I ordered ________ and it sold out in 2 days totally getting more of this!!",
                    BeerId = 1,
                    CustomerId = 1
                },
                new Review
                {
                    Id = 2,
                    Text = "Great beer but I think the delivery driver was drunk.",
                    BeerId = 2,
                    CustomerId = 2
                },
                new Review
                {
                    Id = 3,
                    Text = "10/10 Would not recommend ____ its like they aged it in a sock or something.",
                    BeerId = 3,
                    CustomerId = 3
                },
                new Review
                {
                    Id = 4,
                    Text = "I didn’t believe it when they said they had a non-alcoholic beer that tasted like the real deal but they do!!",
                    BeerId = 4,
                    CustomerId = 1
                },
                new Review
                {
                    Id = 5,
                    Text = "Taste seemed to be pushed by the Simcoe hop more than anything else. " +
                    "Ariana doesn't really play out here beyond adding more of what Simcoe already " +
                    "brings to the table. There is a light stray from the traditional bitter rind " +
                    "notes into more of a fleshy fruit citrus as if there was Citra hops going on. " +
                    "Perhaps the Ariana and Mosaic hops lead to this sum of their parts. Body and" +
                    " yeast was neutral, carbonation fine. Good job on making 8% easily drinkable," +
                    " ibu seemed around 75. Big fail.Pale gold color with a small frothy white head.Good clarity.",
                    BeerId = 5,
                    CustomerId = 2
                },
                new Review
                {
                    Id = 6,
                    Text = "You can tell right off the bat from the aroma that there is a richness to the " +
                    "malt. Lots of fresh bread notes with ample spicy hops. Very rounded in its flavor with" +
                    " moderate malt and hops. Peppery, lightly spicy hops provide a lightly bitter finish" +
                    " but there is good balance overall. Medium bodied.Appearance – The beer is pours a deep " +
                    "copper color with a one finger head of off white foam.The head has a good level of retention, " +
                    "fading slowly over time to leave a small amount of lace on the sides of the glass.",
                    BeerId = 6,
                    CustomerId = 3
                },
                new Review
                {
                    Id = 7,
                    Text = "Overall – A sweeter but rather drinkable strong ale. Certainly, fun to try!",
                    BeerId = 7,
                    CustomerId = 1,
                },
                new Review
                {
                    Id = 8,
                    Text = "I tried this beer at the brewery as part of a four sample flight. The beer " +
                    "poured an opaque dark with very thin tan head that did not leave much ace. The scent " +
                    "had subtle roast and chocolate notes. The taste was nicely balanced and easy to drink " +
                    "with sweet chocolate malt base and roast highlights. The mouthfeel was lighter in body" +
                    " with good carbonation. Overall it was a good beer!",
                    BeerId = 8,
                    CustomerId = 2,
                },
                new Review 
                { 
                    Id = 9,
                    Text = " As the taste approaches the end, it bitters up a bit, taking on more herbal bitterness" +
                    " and a little bit of a cola and nut comes to the tongue, leaving one with a sweet, toasted, and " +
                    "slightly bittered taste to linger on the tongue.",
                    BeerId = 9,
                    CustomerId = 3
                }
                );
        }
    }
}
