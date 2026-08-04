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

        protected override void OnModelCreating(ModelBuilder builder)//seed data for roles
        {
            base.OnModelCreating(builder);
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