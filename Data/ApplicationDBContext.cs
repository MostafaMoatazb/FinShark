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
        :base(dbContextOptions)
        {
            
        }
        public DbSet<Stock> Stocks {get; set;}
        public DbSet<Comment> Comments {get; set;}
        public DbSet<Portfolio> Portfolios {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)//seed data for roles
        {
            base.OnModelCreating(builder);
            builder.Entity<Portfolio>(x => x.HasKey(p => new {p.AppUserId, p.StockId}));
            builder.Entity<Portfolio>()
                .HasOne(p => p.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);
            builder.Entity<Portfolio>()
                .HasOne(p => p.Stock)
                .WithMany(s => s.Portfolios)
                .HasForeignKey(p => p.StockId);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "Admin",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "f64f9cc9-35fa-42d0-a124-84b47aed3932"

                },
                new IdentityRole
                {
                    Id = "User",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "4e7cdeff-1280-4851-9714-e95d6daadf3a"

                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}