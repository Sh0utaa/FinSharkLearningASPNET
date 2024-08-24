using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Shota",
                    NormalizedName = "SHOTA"
                },
                new IdentityRole
                {
                    Name = "Alek",
                    NormalizedName = "ALEK"
                },

            };

            // Seeding Roles data
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Seeding Stock data
            modelBuilder.Entity<Stock>().HasData(
                new Stock
                {
                    Id = 1,
                    Symbol = "AAPL",
                    CompanyName = "Apple Inc.",
                    Purchase = 145.00M,
                    LastDiv = 0.82M,
                    Industry = "Technology",
                    MarketCap = 2230000000000 // in billions
                },
                new Stock
                {
                    Id = 2,
                    Symbol = "MSFT",
                    CompanyName = "Microsoft Corp.",
                    Purchase = 299.00M,
                    LastDiv = 0.62M,
                    Industry = "Technology",
                    MarketCap = 2120000000000 // in billions
                },
                new Stock
                {
                    Id = 3,
                    Symbol = "NVDA",
                    CompanyName = "NVIDIA Corp.",
                    Purchase = 500.00M,
                    LastDiv = 0.40M,
                    Industry = "Semiconductors",
                    MarketCap = 1090000000000 // in billions
                }
            );
        }
    }
}