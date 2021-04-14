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
        }
    }
}
